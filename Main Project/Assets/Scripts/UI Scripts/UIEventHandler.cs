using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(ItemData item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void StatsEventHandler();
    public static event StatsEventHandler OnStatsChanged;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public static void ItemAddedToInventory(ItemData item)
    {
        if (OnItemAddedToInventory != null)
            OnItemAddedToInventory(item);
    }

    public static void ItemAddedToInventory(List<ItemData> items)
    {
        if (OnItemAddedToInventory != null)
        {
            foreach (ItemData item in items)
            {
                OnItemAddedToInventory(item);
            }
        }
    }

    public static void ItemEquipped(EquipmentItemData item)
    {
        if(OnItemEquipped != null)
            OnItemEquipped?.Invoke(item);
    }

    public static void StatsChanged()
    {
        if (OnStatsChanged != null)
            OnStatsChanged();
    }

    public static void PlayerLevelChanged()
    {
        if (OnPlayerLevelChange != null)
            OnPlayerLevelChange?.Invoke();
    }
}
