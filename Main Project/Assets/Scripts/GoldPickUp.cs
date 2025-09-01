using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace oxi
{
    public class GoldPickUp : Interactable
    {
        public int coin = 1;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            playerManager.playerInventory.AddGold(coin);
            Destroy(gameObject);
        }
    }
}
