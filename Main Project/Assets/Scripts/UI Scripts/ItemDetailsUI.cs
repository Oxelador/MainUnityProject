using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    InventorySlot_UI itemSlot;
    ItemData itemData;
    Button itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText;

    private void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent <TextMeshProUGUI>();
        itemInteractButton = transform.Find("Interact_Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetItemDetailsUI(InventorySlot_UI itemSlotUI)
    {
        itemInteractButton.onClick.RemoveAllListeners();
        this.itemSlot = itemSlotUI;
        itemData = itemSlot.AssignedInventorySlot.ItemData;
        if(itemData != null)
        {
            itemNameText.text = itemData.DisplayName;
            itemDescriptionText.text = itemData.Description;
            itemInteractButtonText.text = itemData.ActionName;
            itemInteractButton.onClick.AddListener(OnItemInteract);
        }
    }

    public void OnItemInteract()
    {
        if(itemData.itemType == ItemTypes.Consumable)
        {
            if(itemSlot.AssignedInventorySlot.StackSize > 1)
            {
                PlayerManager.Instance.ConsumeItem(itemData);
                itemSlot.AssignedInventorySlot.RemoveFromStack(1);
                itemSlot.UpdateUISlot();
            }
            else
            {
                PlayerManager.Instance.ConsumeItem(itemData);
                itemSlot.ClearSlot();
            }
        }
        else if(itemData.itemType == ItemTypes.Equipment)
        {
            PlayerManager.Instance.EquipItem(itemData);
            itemSlot.ClearSlot();
        }
    }
}
