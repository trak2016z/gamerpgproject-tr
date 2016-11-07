using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;
public class EquipmentDropController : MonoBehaviour, IDropHandler
{
    protected EquipmentPanelController _EquipmentPanelController;
    void Start()
    {
        _EquipmentPanelController = FindObjectOfType<EquipmentPanelController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var baseComponent = GetComponent<EquipmentSlot>();
        Item thisItem = null;
        if (baseComponent != null)
        {
            thisItem = baseComponent._item;
        }
        var OrginalObject = UIDragController.getPickedObject();
        var pickedItem = OrginalObject.pickableObject as Item;
        var numberOfPickedObjects = OrginalObject.count;
        if (pickedItem != null)
        {
            if (thisItem == null)
            {
                baseComponent._item = pickedItem;
                baseComponent._slotCount = numberOfPickedObjects;
            }
            else if (thisItem.ItemID == pickedItem.ItemID)
            {
                Debug.Log("ID TAKIE SAME");
                var freeSpace = thisItem.MaxStackSize - baseComponent._slotCount;
                Debug.Log("Free Space: " + freeSpace.ToString());
                if (freeSpace > 0)
                {
                    if (freeSpace >= numberOfPickedObjects)
                    {
                        baseComponent._slotCount += numberOfPickedObjects;
                    }
                    else
                    {
                        baseComponent._slotCount = thisItem.MaxStackSize;
                        numberOfPickedObjects -= freeSpace;
                        var tmp = new PickableObject();
                        tmp.pickableObject = pickedItem;
                        tmp.count = numberOfPickedObjects;
                        UIDragController.setPickedObject(tmp);

                    }
                }
                else
                {
                    var tmp = new PickableObject();
                    tmp.pickableObject = pickedItem;
                    tmp.count = numberOfPickedObjects;
                    UIDragController.setPickedObject(tmp);
                }
            }
            else
            {
                var tmp = new PickableObject();
                tmp.pickableObject = thisItem;
                tmp.count = baseComponent._slotCount;

                baseComponent._item = pickedItem;
                baseComponent._slotCount = numberOfPickedObjects;

                UIDragController.setPickedObject(tmp);
            }
        }
        else
        {
            UIDragController.setPickedObject(OrginalObject);
        }

        _EquipmentPanelController.ActualizeViewOfEquipment();
    }
}
