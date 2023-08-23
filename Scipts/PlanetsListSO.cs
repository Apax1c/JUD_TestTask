using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planets", menuName = "PlanetsListSO")]
public class PlanetsListSO : ScriptableObject
{
    public List<GameObject> planets;
}
