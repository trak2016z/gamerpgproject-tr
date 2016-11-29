using UnityEngine;
using System.Collections;
using Items;

public abstract class CharacterPanelItemSlot : MonoBehaviour, IUIPickableComponent {
    public abstract PickableObject getPickableObject();
    public abstract bool setPickableObject(PickableObject obj);
    public abstract void ActualizeViewOfField();
}
