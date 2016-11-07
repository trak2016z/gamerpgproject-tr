using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Character;
public class BarController : MonoBehaviour
{
    private float _LastFrameValue;
    public Text _ValueText;
    public Scrollbar _scrollBar;
    private Statistics updateStatistics;

    public float actualizationTime = 0.25f;

    public void SetColour( Color color ){
        var tmp  = _scrollBar.colors;
        tmp.normalColor = color;
        _scrollBar.colors = tmp;
    }

    public void UpdateValues(float actual, float max)
    {
        if (_LastFrameValue != actual)
        {
            _scrollBar.size = Mathf.Clamp(actual / max, 0.0f, 1.0f);
            _ValueText.text = actual.ToString() + '/' + max.ToString();
            _LastFrameValue = actual;
        }

    }

    protected IEnumerator UpdateStatisticsCorutine()
    {
        while (true)
        {
            UpdateValues(updateStatistics.ActualValue, updateStatistics.MaximalValue);
            yield return new WaitForSeconds(actualizationTime);
        }
    }

    public void SetStatistics(ref Statistics stat)
    {
        if (stat == null) { 
            Debug.Log("Statistic is NULL"); 
        }
        else
        {
            updateStatistics = stat;
            StartCoroutine(UpdateStatisticsCorutine());
        }
    }

}
