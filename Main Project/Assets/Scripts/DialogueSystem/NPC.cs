using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string[] dialogue;
    public string npcName;

    public bool IsInteracted { get; set; }

    private void Start()
    {
        IsInteracted = false;
    }

    public string GetDescription()
    {
        return $"Talk to {npcName}";
    }

    public void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, this);
    }
}
