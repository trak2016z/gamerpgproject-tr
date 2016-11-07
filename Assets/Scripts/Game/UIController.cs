using UnityEngine;
using System.Collections;
using Character;
using System;

public enum MainStatisticsID
{
    Strenght = 0,
    Vitality = 1,
    Inteligence = 2,
    Dexterity = 3,
    Armor = 4,
    MagicResist = 5,
    Health = 6,
    Mana = 7,
    Stamina = 8,
    MovementSpeed = 9
};

public class UIController : MonoBehaviour
{



    public BarController _HealthBar;
    public BarController _ManaBar;
    public BarController _StaminaBar;

    public GameObject _StatisticsPanel;
    public GameObject _InventoryPanel;
    // Use this for initialization
    void Start()
    {
        CharacterDataController characterStatistics;
        try
        {
            characterStatistics = GameObject.Find("Character").GetComponent<CharacterDataController>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Main CHaraacter Not Found in Scene");
            return;
        }
        Statistics statToGet;
        characterStatistics.CharacterStatistics.GetStatistics((int)MainStatisticsID.Health, out statToGet);
        _HealthBar.SetStatistics(ref statToGet);
        characterStatistics.CharacterStatistics.GetStatistics((int)MainStatisticsID.Mana, out statToGet);
        _ManaBar.SetStatistics(ref statToGet);
        characterStatistics.CharacterStatistics.GetStatistics((int)MainStatisticsID.Stamina, out statToGet);
        _StaminaBar.SetStatistics(ref statToGet);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            _StatisticsPanel.SetActive(!_StatisticsPanel.activeSelf);
        }

        if (Input.GetButtonDown("Inventory"))
        {
            _InventoryPanel.SetActive(!_InventoryPanel.activeSelf);
        }
    }
}
