using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;
using Usable;

public class UseBarDropController : MonoBehaviour, IDropHandler {

    protected UseBarPanelController _UseBarPanelController;
    void Start()
    {
        _UseBarPanelController = FindObjectOfType<UseBarPanelController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var baseComponent = GetComponent<UseBarSlot>();
        IPickable thisItem = null;
        #region Sprawdzenie czy komponent jest dodany do pola
        if (baseComponent != null)
        {
            thisItem = baseComponent._item;
        }
        else
        {
            return;
        }
        #endregion
        var OrginalObject = UIDragController.getPickedObject();
        var pickedItem = OrginalObject.pickableObject as IUsable;
        var numberOfPickedObjects = OrginalObject.count;
        if (pickedItem != null)
        {
            if (pickedItem is Item)
            {
                if (thisItem == null) // Sprawdzamy czy jest jeszcze umiejetnoscia!!!!! TO DO
                {
                    baseComponent._item = pickedItem as IPickable;
                    baseComponent._slotCount = numberOfPickedObjects;
                }
                else if (thisItem is Item)
                {
                    if (((Item)thisItem).ItemID == ((Item)pickedItem).ItemID)
                    {
                        Debug.Log("ID TAKIE SAME");
                        var freeSpace = ((Item)thisItem).MaxStackSize - baseComponent._slotCount;
                        Debug.Log("Free Space: " + freeSpace.ToString());
                        if (freeSpace > 0)
                        {
                            if (freeSpace >= numberOfPickedObjects)
                            {
                                baseComponent._slotCount += numberOfPickedObjects;
                            }
                            else
                            {
                                baseComponent._slotCount = ((Item)thisItem).MaxStackSize;
                                numberOfPickedObjects -= freeSpace;
                                var tmp = new PickableObject();
                                tmp.pickableObject = (IPickable)pickedItem;
                                tmp.count = numberOfPickedObjects;
                                UIDragController.setPickedObject(tmp);

                            }
                        }
                        else
                        {
                            var tmp = new PickableObject();
                            tmp.pickableObject = (IPickable)pickedItem;
                            tmp.count = numberOfPickedObjects;
                            UIDragController.setPickedObject(tmp);
                        }
                    }
                    else
                    {
                        var tmp = new PickableObject();
                        tmp.pickableObject = thisItem;
                        tmp.count = baseComponent._slotCount;

                        baseComponent._item = (IPickable)pickedItem;
                        baseComponent._slotCount = numberOfPickedObjects;

                        UIDragController.setPickedObject(tmp);
                    }
                }

            }
            else
            {
                UIDragController.setPickedObject(OrginalObject);
            }
        }
        else
        {
            UIDragController.setPickedObject(OrginalObject);
        }
    }
}
