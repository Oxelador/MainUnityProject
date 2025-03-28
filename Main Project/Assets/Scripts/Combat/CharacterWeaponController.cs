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
            UnequipWeapon();
        }

        //place weapon in hand
        _weaponPrefab = Instantiate((Resources.Load<GameObject>("Weapons/" + itemToEquip.ItemPrefab.name)),
            _playerHand.transform.position,
            _playerHand.transform.rotation);

        //mark item as equipped
        _weaponPrefab.GetComponent<ItemPickUp>().IsInteracted = true;

        //set parent???
        _weaponPrefab.transform.SetParent(_playerHand.transform);

        //get weaponScript from prefab
        _weaponScript = _weaponPrefab.GetComponent<IWeapon>();

        //set statList from ItemData to WeaponScript
        _weaponScript.WeaponStatList = itemToEquip.StatList;

        //add stats from weapon to character
        _stats.AddStatBonus(itemToEquip.StatList);

        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        _stats.RemoveStatBonus(_weaponScript.WeaponStatList);
        Player.Instance.AddToPlayerInventory(_weaponPrefab.GetComponent<ItemPickUp>().ItemData);
        Destroy(_playerHand.transform.GetChild(0).gameObject);
    }

    public void PerformWeaponAttack()
    {
        _weaponScript.PerformAttack(CalculateDamage());
    }

    private float CalculateDamage()
    {
        float damageToDeal = (_stats.GetStat(BaseStatType.Strength).FinalValue * 2) // convert strength into attack power (1 strength == 2 AP)
                                + Random.Range(1, 6); // in future it will be depend on weapon damage range
        damageToDeal += CalculateCrit(damageToDeal);
        return damageToDeal;
    }

    private float CalculateCrit(float damage)
    {
        if (Random.value <= .10f) // .10f - critical chance, will be add to stats later
        {
            float critDamage = damage * .50f; // .5f - critical damage multiplier
            //Debug.Log("Critical damage dealt: " + critDamage);
            return critDamage;
        }
        //Debug.Log("Damage dealt: " + damage);
        return 0;
    }
}

