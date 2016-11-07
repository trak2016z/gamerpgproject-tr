using UnityEngine;
using System.Collections;
using Character;
using UnityEngine.UI;
public class ChangableStatisticsController : MonoBehaviour {

    Statistics _statistic;
	// Use this for initialization

    public Text _StatisticName;
    public BarController _barController;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _barController.UpdateValues(_statistic.ActualValue, _statistic.MaximalValue);
	}

    public void SetStatistic(ref Statistics stat)
    {
        _statistic = stat;
        _StatisticName.text = _statistic.DisplayName;
        _barController.UpdateValues(_statistic.ActualValue, _statistic.MaximalValue);
    }
}
