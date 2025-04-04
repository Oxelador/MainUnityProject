using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public ItemData ItemData;

    private bool isInteracted = false;
    private bool isNear = false;

    public bool IsInteracted
    {
        get => isInteracted;
        set => isInteracted = value;
    }

    public bool IsNear
    {
        get => isNear;
        set => isNear = value;
    }

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        if (IsInteracted) return;

        if (Player.Instance.AddToPlayerInventory(ItemData))
        {
            isInteracted = true;
            Destroy(this.gameObject);
            InteractionButtonsUI.OnButtonRemove?.Invoke(gameObject.GetComponent<IInteractable>());
        }
    }
}
