using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Character;
public class UpgradeStatisticsPanelController : MonoBehaviour {

    CharacterDataController _characterDataController;

    public Transform StrenghtPanel;
    public Transform VitalityPanel;
    public Transform InteligencePanel;
    public Transform DexterityPanel;

    public Transform StatsValuePanel;

	// Use this for initialization
	void Awake() {
        try
        {
            _characterDataController = GameObject.Find("Character").GetComponent<CharacterDataController>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("CharacterNotFound");
        }
        //UpdateDataOnStatisticsPanel();
	}
	
	// Update is called once per frame
	void OnEnable(){
        UpdateDataOnStatisticsPanel();
	}

    void UpdateDataOnStatisticsPanel()
    {
        StrenghtPanel.FindChild("StatisticsValue").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Strenght).MaximalValue.ToString();
        VitalityPanel.FindChild("StatisticsValue").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Vitality).MaximalValue.ToString();
        InteligencePanel.FindChild("StatisticsValue").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Inteligence).MaximalValue.ToString();
        DexterityPanel.FindChild("StatisticsValue").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Dexterity).MaximalValue.ToString();

        StatsValuePanel.FindChild("MagicResist").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.MagicResist).MaximalValue.ToString("0");
        StatsValuePanel.FindChild("Armor").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Armor).MaximalValue.ToString("0");
        StatsValuePanel.FindChild("Health").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Health).MaximalValue.ToString("0");
        StatsValuePanel.FindChild("Mana").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Mana).MaximalValue.ToString("0");
        StatsValuePanel.FindChild("Stamina").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.Stamina).MaximalValue.ToString("0");
        StatsValuePanel.FindChild("MoveSpeed").GetComponent<Text>().text = _characterDataController.CharacterStatistics.FindStatistics((int)MainStatisticsID.MovementSpeed).MaximalValue.ToString("0.00");

    }

    public void UpdateStatisticsLevel(string statistic)
    {
        _characterDataController.CharacterStatistics.FindStatistics(statistic).MaximalValue++;
        _characterDataController.CharacterStatistics.CalculateDependencies();
        UpdateDataOnStatisticsPanel();
    }
}
