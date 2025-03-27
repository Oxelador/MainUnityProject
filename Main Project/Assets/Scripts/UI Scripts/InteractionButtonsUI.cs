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
    }

    public void UpdateUI(HashSet<GameObject> nearbyObjects)
    {
        var buttonsToRemove = new List<GameObject>(buttons.Keys);
        buttonsToRemove.RemoveAll(obj => nearbyObjects.Contains(obj));

        foreach (var obj in buttonsToRemove)
        {
            RemoveButton(obj);
        }

        foreach (var obj in nearbyObjects)
        {
            CreateButton(obj);
        }
    }

    private void CreateButton(GameObject interactionObj)
    {
        if (!buttons.ContainsKey(interactionObj))
        {
            var button = Instantiate(buttonPrefab, contentPanel);
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
        var interactable = interactionObj.GetComponent<IInteractable>();

        if (interactable.IsInteracted)
        {
            Destroy(buttons[interactionObj].gameObject);
            Debug.Log($"Remove from dictionary {buttons[interactionObj]}");
            buttons.Remove(interactionObj);
        }
    }
}
