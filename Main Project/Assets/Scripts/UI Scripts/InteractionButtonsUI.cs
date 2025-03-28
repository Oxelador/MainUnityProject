using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionButtonsUI : MonoBehaviour
{
    public static InteractionButtonsUI Instance { get; set; }

    public GameObject scrollView;
    public Transform contentPanel;
    public Button buttonPrefab;

    public Dictionary<GameObject, Button> buttons = new Dictionary<GameObject, Button>();

    public static UnityAction<GameObject> OnButtonRemove;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        scrollView.SetActive(false);
    }

    public void UpdateUI(HashSet<GameObject> nearbyObjects)
    {
        if(nearbyObjects.Count > 0)
        {
            scrollView.SetActive(true);
            foreach (var obj in nearbyObjects)
            {
                CreateButton(obj);
            }
        }
        else
        {
            foreach (var item in buttons)
            {
                item.Key.GetComponent<IInteractable>().IsNear = false;
            }
            scrollView.SetActive(false);
        }

        var objectsToRemove = new List<GameObject>();

        foreach (var obj in buttons)
        {
            if(obj.Key.GetComponent<IInteractable>().IsInteracted || !obj.Key.GetComponent<IInteractable>().IsNear)
            {
                objectsToRemove.Add(obj.Key);
            }
        }

        foreach (var obj in objectsToRemove)
        {
            RemoveButton(obj);
        }
    }

    private void CreateButton(GameObject interactionObj)
    {
        if (!buttons.ContainsKey(interactionObj))
        {
            var button = Instantiate(buttonPrefab, contentPanel);
            interactionObj.GetComponent<IInteractable>().IsNear = true;
            buttons.Add(interactionObj, button);
            Debug.Log($"Add into dictionary {buttons[interactionObj]}");
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                var interactable = interactionObj.GetComponent<IInteractable>();
                OnButtonRemove += RemoveButton;
                interactable.Interact();
            });
        }
    }

    private void RemoveButton(GameObject interactionObj)
    {
        if (buttons.ContainsKey(interactionObj))
        {
            var interactable = interactionObj.GetComponent<IInteractable>();

            if (interactable.IsInteracted || !interactable.IsNear)
            {
                Destroy(buttons[interactionObj].gameObject);
                Debug.Log($"Remove from dictionary {buttons[interactionObj]}");
                buttons.Remove(interactionObj);
            }
        }
        else
        {
            //Debug.LogWarning($"Key {interactionObj.name} not found in dictionary.");
        }
    }
}
