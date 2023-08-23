using System.Collections.Generic;

public interface IPlaneterySystem
{
    public IEnumerable<IPlaneteryObject> PlaneteryObjects { get; set; }
    public void UpdatePlaneterySystem(float deltaTime);
}