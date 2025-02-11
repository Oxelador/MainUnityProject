using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    // TODO: system menu (now its only saveloadbuttons holder)
    public GameObject saveLoadButtons;

    private void Awake()
    {
        inventoryPanel.transform.parent.gameObject.SetActive(false);
        playerBackpackPanel.transform.parent.gameObject.SetActive(false);
        saveLoadButtons.gameObject.SetActive(false);
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

    public void OpenSystemMenu()
    {
        saveLoadButtons.gameObject.SetActive(true);
    }

    public void CloseSystemMenu()
    {
        saveLoadButtons.gameObject.SetActive(false);
    }

    public void ClosePlayerBackpack()
    {
        playerBackpackPanel.transform.parent.gameObject.SetActive(false);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.transform.parent.gameObject.SetActive(false);
    }

    private void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
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
}
