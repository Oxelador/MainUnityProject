using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string[] dialogue;
    public string npcName;

    private bool isInteracted = false;

    public bool IsInteracted
    {
        get => isInteracted;
        set => isInteracted = value;
    }

    public void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
    }
}
