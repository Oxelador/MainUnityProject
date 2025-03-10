using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    InventorySlot_UI itemSlot;
    ItemData itemData;
    Image itemIcon;
    Button itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText;

    public TextMeshProUGUI textStat;

    private void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent <TextMeshProUGUI>();
        itemInteractButton = transform.Find("Interact_Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        itemIcon = transform.Find("Item_Icon").GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetItemDetailsUI(InventorySlot_UI itemSlotUI)
    {
        if (itemSlotUI.AssignedInventorySlot.ItemData != null)
        {
            gameObject.SetActive(true);
            textStat.text = "";
            itemInteractButton.onClick.RemoveAllListeners();
            this.itemSlot = itemSlotUI;
            itemData = itemSlot.AssignedInventorySlot.ItemData;
            if (itemData != null)
            {
                itemIcon.sprite = itemData.Icon;
                itemNameText.text = itemData.DisplayName;
                itemDescriptionText.text = itemData.Description;
                itemInteractButtonText.text = itemData.ActionName;
                itemInteractButton.onClick.AddListener(OnItemInteract);

                if(itemData is EquipmentItemData equipmentItemData)
                {
                    foreach (BaseStat stat in equipmentItemData.StatList)
                    {
                        textStat.text += $"{stat.StatType}: {stat.BaseValue}\n";
                    }
                }
                else if(itemData is ConsumableItemData consumableItemData)
                {
                    //TODO: display stats or actions for consumable items
                }

            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnItemInteract()
    {
        if(itemData.itemType == ItemTypes.Consumable)
        {
            if(itemSlot.AssignedInventorySlot.StackSize > 1)
            {
                Player.Instance.ConsumeItem(itemData);
                itemSlot.AssignedInventorySlot.RemoveFromStack(1);
                itemSlot.UpdateUISlot();
            }
            else
            {
                Player.Instance.ConsumeItem(itemData);
                itemSlot.ClearSlot();
            }
        }
        else if(itemData.itemType == ItemTypes.Equipment)
        {
            Player.Instance.EquipItem(itemData);
            itemSlot.ClearSlot();
        }
        RemoveItem();
    }

    public void RemoveItem()
    {
        itemSlot = null;
        gameObject.SetActive(false);
    }
}
