using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string npcName;

    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
    }
}
