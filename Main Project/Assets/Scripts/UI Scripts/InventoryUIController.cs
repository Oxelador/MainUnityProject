using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    private void Awake()
    {
        inventoryPanel.transform.parent.gameObject.SetActive(false);
        playerBackpackPanel.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;

    }

    private void DisplayInventory(InventorySystem invToDisplay)
    {
        if (!inventoryPanel.transform.parent.gameObject.activeInHierarchy)
        {
            inventoryPanel.transform.parent.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invToDisplay);
        }
        else
        {
            CloseInventoryPanel();
        }
    }

    private void DisplayPlayerInventory(InventorySystem invToDisplay)
    {
        if (!playerBackpackPanel.transform.parent.gameObject.activeInHierarchy)
        {
            playerBackpackPanel.transform.parent.gameObject.SetActive(true);
            playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
        }
        else
        {
            ClosePlayerBackpack();
        }
    }

    public void ClosePlayerBackpack()
    {
        playerBackpackPanel.transform.parent.gameObject.SetActive(false);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.transform.parent.gameObject.SetActive(false);
    }
}
