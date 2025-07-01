using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public string lootName;
    public GameObject lootPrefab;
    public int dropChance;
}
