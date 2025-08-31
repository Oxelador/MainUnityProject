using UnityEngine;

namespace oxi
{
    [CreateAssetMenu(menuName = "Items/Loot Item")]
    public class Loot : Item
    {
        public GameObject modelPrefab;
        public int dropChance;
    }
}
