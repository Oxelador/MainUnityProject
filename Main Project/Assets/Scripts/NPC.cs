using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string[] dialogue;
    public string npcName;

    public void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
    }
}
