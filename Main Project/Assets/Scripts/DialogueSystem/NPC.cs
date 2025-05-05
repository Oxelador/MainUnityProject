using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] dialogue;
    public string npcName;

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

    public void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
    }
}
