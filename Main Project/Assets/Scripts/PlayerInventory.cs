using UnityEngine;
using UnityEngine.Events;

namespace oxi
{
    public class PlayerInventory : MonoBehaviour
    {
        public static readonly UnityEvent<int> OnGoldAdd = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnGoldRemove = new UnityEvent<int>();

        public int Gold
        {
            get { return gold; }
            private set
            {
                gold = value;
                UIEventHandler.OnGoldDisplayUpdate.Invoke(gold);
            }
        }

        int gold;

        private void Start()
        {
            Gold = 0;
            OnGoldAdd.Invoke(0);
        }

        public void AddGold(int amount)
        {
            if (amount <= 0) return;
            Gold += amount;
        }

        public void RemoveGold(int amount)
        {
            if (amount <= 0) return;
            Gold -= amount;
        }

        private void OnEnable()
        {
            OnGoldAdd.AddListener(AddGold);
            OnGoldRemove.AddListener(RemoveGold);
        }

        private void OnDisable()
        {
            OnGoldAdd.RemoveListener(AddGold);
            OnGoldRemove.RemoveListener(RemoveGold);
        }
    }
}
