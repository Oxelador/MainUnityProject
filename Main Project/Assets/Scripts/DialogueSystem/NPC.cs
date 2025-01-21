using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string[] dialogue;
    public string npcName;

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
        interactSuccessful = true;
    }

    public void EndInteraction()
    {

    }
}
