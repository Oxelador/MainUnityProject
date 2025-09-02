using oxi;
using UnityEngine;

namespace oxi {
    public class NPC : Interactable
    {
        public string[] dialogue;
        public string npcName;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            DialogueSystem.Instance.AddNewDialogue(dialogue, this);
        }
    }
}
