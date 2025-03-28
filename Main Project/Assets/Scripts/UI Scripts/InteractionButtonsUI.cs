using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionButtonsUI : MonoBehaviour
{
    public static InteractionButtonsUI Instance { get; set; }

    public GameObject scrollView;
    public Transform contentPanel;

    public Button pickUpButtonPrefab;
    public Button talkButtonPrefab;
    public Button chestButtonPrefab;

    public Dictionary<IInteractable, Button> buttons = new Dictionary<IInteractable, Button>();

    public static UnityAction<IInteractable> OnButtonRemove;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        scrollView.SetActive(false);
    }

    public void UpdateUI(HashSet<IInteractable> nearbyInteractableObjects)
    {
        if (nearbyInteractableObjects.Count > 0)
        {
            scrollView.SetActive(true);
            foreach (var obj in nearbyInteractableObjects)
            {
                CreateButton(obj);
            }
        }
        else
        {
            foreach (var item in buttons)
            {
                item.Key.IsNear = false;
            }
            scrollView.SetActive(false);
        }

        var objectsToRemove = new List<IInteractable>();

        foreach (var obj in buttons)
        {
            if (obj.Key.IsInteracted || !obj.Key.IsNear)
            {
                objectsToRemove.Add(obj.Key);
            }
        }

        foreach (var obj in objectsToRemove)
        {
            RemoveButton(obj);
        }
    }

    private void CreateButton(IInteractable interactable)
    {
        if (!buttons.ContainsKey(interactable))
        {
            var button = InstantiateButton(interactable);
            interactable.IsNear = true;
            buttons.Add(interactable, button);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnButtonRemove += RemoveButton;
                interactable.Interact();
            });
        }
    }

    private void RemoveButton(IInteractable interactable)
    {
        if (buttons.ContainsKey(interactable))
        {
            if (interactable.IsInteracted || !interactable.IsNear)
            {
                Destroy(buttons[interactable].gameObject);
                buttons.Remove(interactable);
            }
        }
        else
        {
            //Debug.LogWarning($"Key {interactionObj.name} not found in dictionary.");
        }
    }

    private Button InstantiateButton(IInteractable obj)
    {
        if (obj is ItemPickUp)
        {
            return Instantiate(pickUpButtonPrefab, contentPanel);
        } 
        else if (obj is NPC)
        {
            return Instantiate(talkButtonPrefab, contentPanel);
        }
        else if(obj is ChestInventory)
        {
            return Instantiate(chestButtonPrefab, contentPanel);
        }

        return null;
    }
}
