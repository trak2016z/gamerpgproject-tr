using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Items;
public class CharacterItemsPanelDropController : MonoBehaviour, IDropHandler {


    public void OnDrop(PointerEventData eventData)
    {
        var baseComponent = GetComponent<CharacterPanelItemSlot>();
        if (baseComponent != null)
        {
            var orginalObject = UIDragController.getPickedObject();
            if (orginalObject.pickableObject is Item)
            {
                var pickedObject = orginalObject.pickableObject as Item;
                if (baseComponent.CanItemBeAdded(pickedObject))
                {
                    baseComponent.AddItem(pickedObject);
                }
                else
                {
                    UIDragController.setPickedObject(orginalObject);
                }
            }
            else
            {
                UIDragController.setPickedObject(orginalObject);
            }
            baseComponent.ActualizeViewOfSlot();
        }

    }

}
