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

	void OnDisable(){
		var tmp = FindObjectOfType<ItemsDataPanelController> ();
		if (tmp != null) {
			tmp.IsObjectUsing = false;
		}
	}

    void Start()
    {
        for (int a = 0; a < 10; a++)
        {
            var item = ItemsStorage.getInstace().GetItem(ItemTypes.Potion, 4);
            AddItemToEquipment(new PickableObject() { pickableObject = item, count = 10 });
        }
        AddItemToEquipment(new PickableObject() { pickableObject = ItemsStorage.getInstace().GetItem(ItemTypes.Weapon, 1), count = 1 });
        AddItemToEquipment(new PickableObject() { pickableObject = ItemsStorage.getInstace().GetItem(ItemTypes.Weapon, 2), count = 1 });


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

    public bool AddItemToEquipment(PickableObject _object)
    {
        Debug.Log("AddingObject: " + _object.pickableObject.ToString() + "  " + _object.count.ToString());
        var item = _object.pickableObject as Item;
        var count = _object.count;
        if (item != null)
        {
            for (int a = 0; a < _equipmentFields.Count; a++)
            {
                if (_equipmentFields[a].GetComponent<EquipmentSlot>()._item != null)
                {
                    continue;
                }
                else
                {
                    _equipmentFields[a].GetComponent<EquipmentSlot>()._item = item;
                    _equipmentFields[a].GetComponent<EquipmentSlot>()._slotCount = count;
                    _equipmentFields[a].GetComponent<EquipmentSlot>().ActualizeViewOfField();
                    return true;
                }
            }
        }
        return false;
    }
}
