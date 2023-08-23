using System.Collections;
using UnityEngine;

public class PlaneterySystemFactoryBehaviour : MonoBehaviour, IPlaneterySystemFactory
{
    [Tooltip("Prefab of the Sun object, that will be the center of the planetery system")]
    public GameObject sunPrefab;

    private GameObject newPlaneterySystemGO;

    private void Start()
    {
        double startMass = Random.Range(0.1f, 10000);
        Create(startMass);
    }

    public IPlaneterySystem Create(double mass)
    {
        // Create new GameObject (PlaneterySystem)
        newPlaneterySystemGO = new GameObject();
        newPlaneterySystemGO.name = "PlaneterySystem";
        newPlaneterySystemGO.transform.position = Vector3.zero;

        // Instantiate Sun in center of system
        Instantiate(sunPrefab, newPlaneterySystemGO.transform);

        // Add necessary scripts and initialize planetery system
        newPlaneterySystemGO.AddComponent<PlaneterySystemBehaviour>();
        newPlaneterySystemGO.GetComponent<PlaneterySystemBehaviour>().InitializeSystem(mass);
        return newPlaneterySystemGO.GetComponent<PlaneterySystemBehaviour>();
    }

    public void NewPlaneterySystem()
    {
        StartCoroutine(CreateNextSystem());
    }

    // Recreating planetery system with random mass
    private IEnumerator CreateNextSystem()
    {
        Destroy(newPlaneterySystemGO);
        Create(Random.Range(0.1f, 10000));

        yield return new WaitForSeconds(1);
    }
}