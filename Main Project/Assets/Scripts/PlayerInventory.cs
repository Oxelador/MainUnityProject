using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace oxi
{
    public class PlayerInventory : MonoBehaviour
    {
        public TextMeshProUGUI goldAmountUI;

        int gold;

        private void Awake()
        {
            goldAmountUI.text = gold.ToString(); 
        }

        public void AddGold(int amount)
        {
            if (amount <= 0) return;

            gold += amount;
            goldAmountUI.text = gold.ToString();
        }

        public void RemoveGold(int amount)
        {
            if (amount <= 0) return;

            gold -= amount;
            goldAmountUI.text = gold.ToString();
        }
    }
}
