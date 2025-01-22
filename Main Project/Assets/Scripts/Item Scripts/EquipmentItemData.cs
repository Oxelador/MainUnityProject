using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Equipment Item")]
public class EquipmentItemData : ItemData
{
    [Header("Equipment Data")]
    public bool ItemModifier; // TODO: item modifier for consumable items
    public List<BaseStat> Stats;
}
