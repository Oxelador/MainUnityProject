using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Equipment Item")]
public class EquipmentItemData : ItemData
{
    [Header("Equipment Data")]
    public List<BaseStat> StatList;
}
