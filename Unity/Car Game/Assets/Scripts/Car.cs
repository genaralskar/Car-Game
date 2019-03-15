using System.Collections;
using UnityEngine;

namespace genaralskar
{
    /// <summary>
    /// Contains information about a car, such as it's prefab, name, flavor text, and armors
    /// </summary>
    [CreateAssetMenu(menuName = "Car/New Car")]
    public class Car : ScriptableObject
    {
        public GameObject carPrefab;
        public string carName;
        public string carFlavor;
        public Armor[] armors;
    }
}
