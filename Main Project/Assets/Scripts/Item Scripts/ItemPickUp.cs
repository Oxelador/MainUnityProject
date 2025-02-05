using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRange = 3f;
    public ItemData ItemData;

    private SphereCollider myCollider;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string _id;

    private void Awake()
    {
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);

        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRange;
    }

    private void Start()
    {
        _id = GetComponent<UniqueID>().ID;
        SaveGameManager.data.activeItems.Add(_id, itemSaveData);
    }

    private void LoadGame(SaveData data)
    {
        if(data.collectedItems.Contains(_id)) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(SaveGameManager.data.activeItems.ContainsKey(_id)) SaveGameManager.data.activeItems.Remove(_id);
        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        if(!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            SaveGameManager.data.collectedItems.Add(_id);
            Destroy(this.gameObject);
        }
    }
}

[Serializable]
public struct ItemPickUpSaveData
{
    public ItemData ItemData;
    public Vector3 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(ItemData data, Vector3 position, Quaternion rotation)
    {
        ItemData = data;
        Position = position;
        Rotation = rotation;
    }
}
