using UnityEngine;
using System.Collections;
using Character;
using UnityEngine.UI;

public class IncreasableStatisticsController : MonoBehaviour {

    Statistics _statistic;

    public Text _LevelValue;
    public Text _StatisticsText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetStatistic(ref Statistics statistic)
    {
        _statistic = statistic;
        _LevelValue.text = _statistic.ActualValue.ToString();
        _StatisticsText.text = _statistic.DisplayName;
    }

    public void IcreaseStatisticsLevel()
    {
        // TO DO
    }

}
