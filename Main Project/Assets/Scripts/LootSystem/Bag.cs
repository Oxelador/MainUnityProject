using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    public static readonly UnityEvent<int> OnGoldAdd = new UnityEvent<int>();
    public static readonly UnityEvent<int> OnGoldRemove = new UnityEvent<int>();

    private int _gold;

    public int Gold
    {
        get { return _gold; }
        private set
        {
            _gold = value;
            UIEventHandler.OnGoldDisplayUpdate.Invoke(_gold);
        }
    }

    private void Start()
    {
        Gold = 0;
        OnGoldAdd.Invoke(0);
    }

    private void AddGold(int amount)
    {
        if (amount <= 0) return;
        Gold += amount;
    }

    private void RemoveGold(int amount)
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
