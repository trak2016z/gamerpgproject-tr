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
            UIDragController.ShouldBeDroped = false;
            var orginalObject = UIDragController.getPickedObject();
			var actualObject = baseComponent.getPickableObject ();
            if (baseComponent.setPickableObject(orginalObject))
            {
				UIDragController.setPickedObject (actualObject);
            }
            else
            {
                UIDragController.setPickedObject(orginalObject);
            }
            baseComponent.ActualizeViewOfField();
        }

    }

}
