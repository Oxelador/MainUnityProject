using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetailsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name, _level;
    [SerializeField] private Image _levelFill;

    // stats
    private List<TextMeshProUGUI> _statList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _statPrefab;
    [SerializeField] private Transform _statPanel;

    // equipped weapon
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TextMeshProUGUI _weaponText;

    void Start()
    {
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;

        InitializeStats();
    }

    void InitializeStats()
    {
        for(int i = 0; i < Player.Instance.Stats.statsList.Count; i++)
        {
            _statList.Add(Instantiate(_statPrefab));
            _statList[i].transform.SetParent(_statPanel, false);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < Player.Instance.Stats.statsList.Count; i++)
        {
            _statList[i].text = Player.Instance.Stats.statsList[i].StatType.ToString() + 
                                ": " + 
                                Player.Instance.Stats.statsList[i].FinalValue;
        }
    }

    void UpdateEquippedWeapon(ItemData item)
    {
        _weaponIcon.sprite = item.Icon;
        _weaponText.text = item.DisplayName;
    }

    public void UnequipWeapon()
    {
        _weaponIcon.sprite = null;
        _weaponText.text = "-";
        Player.Instance.CharacterWeaponController.UnequipWeapon();
    }
}
