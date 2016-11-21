using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Items;

public class EquipmentPanelController : MonoBehaviour
{
    protected List<Transform> _equipmentFields;
    UseBarPanelController _useBarPanelController;
    void Awake()
    {
        _equipmentFields = new List<Transform>();
        for (int a = 0; a < transform.childCount; a++)
        {
            _equipmentFields.Add(transform.GetChild(a));
        }
    }

    void OnEnable()
    {
        ActualizeViewOfEquipment();
    }


    public void ChangeItemsPlaces(Transform firstObject, Transform secondObject)
    {
        Debug.Log("Podmieniam");
        var firstItem = firstObject.GetComponent<EquipmentSlot>();
        var secondItem = secondObject.GetComponent<EquipmentSlot>();
        if (secondItem._item == null) { 
            return;
        }else
        if (firstItem._item == null)
        {
            firstItem._item = secondItem._item;
            firstItem._slotCount = secondItem._slotCount;
            secondItem._item = null;
            secondItem._slotCount = -1;
        }else
        if (firstItem._item.ItemID == secondItem._item.ItemID)
        {
            Debug.Log("ID TAKIE SAME");
            var freeSpace = firstItem._item.MaxStackSize - firstItem._slotCount;
            Debug.Log("freeSpace: " + freeSpace.ToString());
            if (freeSpace > 0)
            {
                if (freeSpace >= secondItem._slotCount)
                {
                    firstItem._slotCount += secondItem._slotCount;

                    secondItem._item = null;
                    secondItem._slotCount = -1;
                }
                else
                {
                    firstItem._slotCount = firstItem._item.MaxStackSize;
                    secondItem._slotCount -= freeSpace;

                }
            }
        }
        else
        {
            var tmpItem = firstItem._item;
            var tmpSlotCount = firstItem._slotCount;
            firstItem._item = secondItem._item;
            firstItem._slotCount = secondItem._slotCount;
            secondItem._item = tmpItem;
            secondItem._slotCount = tmpSlotCount;
        }

        ActualizeViewOfEquipment();
    }

    public void ActualizeViewOfEquipment()
    {
        for (int count = 0; count < _equipmentFields.Count; count++)
        {
            _equipmentFields[count].GetComponent<EquipmentSlot>().ActualizeViewOfField();
        }
    }

    public bool AddItemToEquipment(Item item, int count)
    {
        for (int a = 0; a < _equipmentFields.Count; a++)
        {
            if(_equipmentFields[a].GetComponent<EquipmentSlot>()._item != null){ 
                continue; 
            }
            else{
                _equipmentFields[a].GetComponent<EquipmentSlot>()._item = item;
                _equipmentFields[a].GetComponent<EquipmentSlot>()._slotCount = count;

                return true;
            }
        }
        return false;
    }
}
