using System.Collections.Generic;
using UnityEngine;

public enum MassClassEnum
{
    Asteroidan,
    Mercurian,
    Subterran,
    Terran,
    Superterran,
    Neptunian,
    Jovian
}

public struct PlanetData
{
    public MassClassEnum massClass;
    public double planetMass;
    public float radius;
}

public class PlaneterySystemBehaviour : MonoBehaviour, IPlaneterySystem
{
    private PlanetsListSO planetsPrefabsList;

    public IEnumerable<IPlaneteryObject> PlaneteryObjects { get => planeteryObjects; set => planeteryObjects = value; }
    private IEnumerable<IPlaneteryObject> planeteryObjects;

    private float rotationSpeed = 10;

    private void Update()
    {
        UpdatePlaneterySystem(Time.deltaTime);
    }

    public void UpdatePlaneterySystem(float deltaTime)
    {
        transform.RotateAround(transform.position, transform.up, rotationSpeed * deltaTime);
    }

    public void InitializeSystem(double mass)
    {
        // Load list of planets prefabs
        planetsPrefabsList = Resources.Load<PlanetsListSO>("Planets");
        List<PlanetData> planetToSpawn = GetListRandomPlanet(mass);

        for (int i = 0; i < planetToSpawn.Count - 1; i++)
        {
            // Create new GO (parent of the planet)
            GameObject centerOfPlaneterySystem = new();
            centerOfPlaneterySystem.transform.parent = transform;
            centerOfPlaneterySystem.name = "Planet(" + i + ")";
            centerOfPlaneterySystem.AddComponent<PlaneteryObjectBehaviour>();

            // Instantiate new planet
            centerOfPlaneterySystem.transform.Rotate(0, Random.Range(0, 360), 0);
            centerOfPlaneterySystem.GetComponent<PlaneteryObjectBehaviour>().planet = Instantiate(planetsPrefabsList.planets[Random.Range(0, planetsPrefabsList.planets.Count - 1)], centerOfPlaneterySystem.transform);
            centerOfPlaneterySystem.GetComponent<PlaneteryObjectBehaviour>().planet.transform.localPosition = new Vector3(0, 0, planetToSpawn[i].radius * 10);
            centerOfPlaneterySystem.GetComponent<PlaneteryObjectBehaviour>().InitializePlanet(planetToSpawn[i].massClass, planetToSpawn[i].planetMass, planetToSpawn[i].radius);
        }
    }

    /// <summary>
    /// Returns list of random planets with fixed system mass
    /// </summary>
    /// <param name="systemMass"></param>
    /// <returns>PlanetData List</returns>
    private List<PlanetData> GetListRandomPlanet(double systemMass)
    {
        // Initialize new Planet Data List with fixed total mass
        List<PlanetData> planetDataList = new();
        int planetCount = Random.Range(2, planetsPrefabsList.planets.Count);
        double leftMass = systemMass;

        while (planetCount > 0)
        {
            planetCount--;

            // Initialize new planet
            PlanetData planetData = new();
            planetData.massClass = (MassClassEnum)Random.Range(0, 6);
            planetData.planetMass = GetRandomMass(planetData.massClass);
            if (leftMass != 0 && leftMass - planetData.planetMass < 0) 
                planetData.planetMass = leftMass;
            planetData.radius = GetRandomRadius(planetData.massClass);

            // Add initialized planet in Planet Data List
            planetDataList.Add(planetData);
            leftMass -= planetData.planetMass;

            if (leftMass <= 0) break;
        }

        return planetDataList;
    }

    /// <summary>
    /// Returns random mass of the planet based on its class
    /// </summary>
    /// <param name="massClass"></param>
    /// <returns>double</returns>
    private double GetRandomMass(MassClassEnum massClass)
    {
        double planetMass = 0;

        switch (massClass)
        {
            case MassClassEnum.Asteroidan:
                planetMass = Random.Range(0, 0.00001f);
                break;
            case MassClassEnum.Mercurian:
                planetMass = Random.Range(0.00001f, 0.1f);
                break;
            case MassClassEnum.Subterran:
                planetMass = Random.Range(0.1f, 0.5f);
                break;
            case MassClassEnum.Terran:
                planetMass = Random.Range(0.5f, 2f);
                break;
            case MassClassEnum.Superterran:
                planetMass = Random.Range(2f, 10f);
                break;
            case MassClassEnum.Neptunian:
                planetMass = Random.Range(10f, 50f);
                break;
            case MassClassEnum.Jovian:
                planetMass = Random.Range(50f, 5000f);
                break;
        }

        return planetMass;
    }

    /// <summary>
    /// Returns random radius of the planet based on its class
    /// </summary>
    /// <param name="massClass"></param>
    /// <returns>float</returns>
    private float GetRandomRadius(MassClassEnum massClass)
    {
        float planetRadius = 0;

        switch (massClass)
        {
            case MassClassEnum.Asteroidan:
                planetRadius = Random.Range(0f, 0.03f);
                break;
            case MassClassEnum.Mercurian:
                planetRadius = Random.Range(0.03f, 0.7f);
                break;
            case MassClassEnum.Subterran:
                planetRadius = Random.Range(0.5f, 1.2f);
                break;
            case MassClassEnum.Terran:
                planetRadius = Random.Range(0.8f, 1.9f);
                break;
            case MassClassEnum.Superterran:
                planetRadius = Random.Range(1.3f, 3.3f);
                break;
            case MassClassEnum.Neptunian:
                planetRadius = Random.Range(2.1f, 5.7f);
                break;
            case MassClassEnum.Jovian:
                planetRadius = Random.Range(3.5f, 27f);
                break;
        }

        return planetRadius;
    }
}