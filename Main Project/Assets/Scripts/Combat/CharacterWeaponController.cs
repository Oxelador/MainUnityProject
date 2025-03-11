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
        if(Random.value <= .10f) // .10f - critical chance, will be add to stats later
        {
            float critDamage = damage * .50f; // .5f - critical damage multiplier
            Debug.Log("Critical damage dealt: " + critDamage);
            return critDamage;
        }
        Debug.Log("Damage dealt: " + damage);
        return 0;
    }
}

