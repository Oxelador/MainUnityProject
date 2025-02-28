using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    //new

    [SerializeField] private GameObject _playerHand;
    private GameObject _weaponObject;
    private Stats _stats;
    private IWeapon _weaponScript;

    //old
    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _stats = GetComponent<Stats>();
        _stats.stats[1].CalculateStatValue();
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
        if(_weaponObject != null)
        {
            _stats.RemoveStatBonus(_weaponObject.GetComponent<IWeapon>().Stats);
            Destroy(_playerHand.transform.GetChild(0).gameObject);
        }

        _weaponObject = Instantiate((Resources.Load<GameObject>("Weapons/" + itemToEquip.ItemPrefab.name)),
            _playerHand.transform.position, 
            _playerHand.transform.rotation);

        _weaponObject.GetComponent<SphereCollider>().enabled = false;
        _weaponObject.GetComponent<ItemPickUp>().EquipItem();

        _weaponScript = _weaponObject.GetComponent<IWeapon>();
        _weaponScript.Stats = itemToEquip.Stats;
        _weaponObject.transform.SetParent(_playerHand.transform);
        _stats.AddStatBonus(itemToEquip.Stats);
        _stats.stats[1].CalculateStatValue();
        //Debug.Log($"Item: {_weaponObject.name} with stats {_weaponScript.Stats[0].StatName} {_weaponScript.Stats[0].BaseValue} equipped.");
    }
    public void PerformWeaponAttack()
    {
        _weaponObject.GetComponent<IWeapon>().PerformAttack();
    }
}

