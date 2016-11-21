using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseBarPanelController : MonoBehaviour
{

    List<Transform> _useBarFields;
    // Use this for initialization
    void Awake()
    {
        _useBarFields = new List<Transform>();
        for (int a = 0; a < transform.childCount; a++)
        {
            _useBarFields.Add(transform.GetChild(a));
        }
    }

    void Update()
    {
        ActualizeViewOfUseBar();
    }

    public void ActualizeViewOfUseBar()
    {
        for (int count = 0; count < _useBarFields.Count; count++)
        {
            _useBarFields[count].GetComponent<UseBarSlot>().ActualizeViewOfField();
        }
    }
}
