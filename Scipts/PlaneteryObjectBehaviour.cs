using UnityEngine;

public class PlaneteryObjectBehaviour : MonoBehaviour, IPlaneteryObject
{
    public MassClassEnum MassClass { get => planetMassClass; set => planetMassClass = value; }
    public double Mass { get => planetMass; set => planetMass = value; }

    private MassClassEnum planetMassClass;
    private double planetMass;
    private float planetRadius;

    public GameObject planet;

    public void InitializePlanet(MassClassEnum classObject, double massObject, float planetRadius)
    {
        this.planetRadius = planetRadius;
        planet.transform.localScale = Vector3.one * this.planetRadius;
        planetMass = massObject;
        planetMassClass = classObject;
    }
}