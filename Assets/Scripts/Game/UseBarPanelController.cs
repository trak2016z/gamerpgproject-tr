using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseBarPanelController : MonoBehaviour
{

    List<Transform> _useBarFields;
    EquipmentPanelController _equipmentPanelController;
    // Use this for initialization
    void Awake()
    {
        _useBarFields = new List<Transform>();
        for (int a = 0; a < transform.childCount; a++)
        {
            _useBarFields.Add(transform.GetChild(a));
        }
    }

    void Start()
    {
        var objs = FindObjectOfType<EquipmentPanelController>();
        if (objs != null)
        {
            _equipmentPanelController = objs;
        }
    }

    public int GetIndexOfTransform(Transform objToFind)
    {
        var tmp = _useBarFields.IndexOf(objToFind);
        if (tmp != -1)
        {
            return tmp;
        }
        else
        {
            return _equipmentPanelController.GetIndexOfTransform(objToFind);
        }
    }

}
