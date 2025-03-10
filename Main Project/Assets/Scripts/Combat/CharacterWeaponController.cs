using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _playerHand;
    private GameObject _weaponPrefab;
    private Stats _stats;
    private IWeapon _weaponScript;

    private void Start()
    {
        _stats = GetComponent<Stats>();
        //_stats.DisplayStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformWeaponAttack();
        }
    }

    public void EquipWeapon(EquipmentItemData itemToEquip)
    {
        if (_weaponPrefab != null)
        {
            _stats.RemoveStatBonus(_weaponPrefab.GetComponent<IWeapon>().WeaponStatList);
            Destroy(_playerHand.transform.GetChild(0).gameObject);
        }

        //place weapon in hand
        _weaponPrefab = Instantiate((Resources.Load<GameObject>("Weapons/" + itemToEquip.ItemPrefab.name)),
            _playerHand.transform.position,
            _playerHand.transform.rotation);

        //mark item as equiped and disable pickUp collider
        _weaponPrefab.GetComponent<ItemPickUp>().EquipItem();

        //set parent???
        _weaponPrefab.transform.SetParent(_playerHand.transform);

        //get weaponScript from prefab
        _weaponScript = _weaponPrefab.GetComponent<IWeapon>();

        //add stats from weapon to character
        _stats.AddStatBonus(itemToEquip.StatList);

        //set CharacterStatList to weaponScript statList
        _weaponScript.WeaponStatList = _stats.statsList;
    }

    public void PerformWeaponAttack()
    {
        _weaponPrefab.GetComponent<IWeapon>().PerformAttack();
    }
}

