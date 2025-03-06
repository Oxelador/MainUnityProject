using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    //new

    [SerializeField] private GameObject _playerHand;
    private GameObject _weaponPrefab;
    private Stats _stats;
    private IWeapon _weaponScript;

    //old
    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _stats = GetComponent<Stats>();
        //_stats.DisplayStats();
    }

    private void Update()
    {
        if(tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PerformWeaponAttack();
            }
        }
        else if(tag == "Enemy")
        {
            if (!_enemyController.IsCaughtUp)
            {
                PerformWeaponAttack();
            }
        }
    }

    public void EquipWeapon(EquipmentItemData itemToEquip)
    {
        if(_weaponPrefab != null)
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

        Debug.Log("Strength in WeaponControllerScript is " + _weaponScript.WeaponStatList.Find(stat => stat.StatType == BaseStatType.Strength));

    }
    public void PerformWeaponAttack()
    {
        _weaponPrefab.GetComponent<IWeapon>().PerformAttack();
    }
}

