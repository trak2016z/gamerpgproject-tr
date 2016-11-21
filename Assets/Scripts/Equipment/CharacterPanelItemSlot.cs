using UnityEngine;
using System.Collections;
using Items;

public abstract class CharacterPanelItemSlot : MonoBehaviour {

    public abstract bool CanItemBeAdded(Item item);

    public abstract void AddItem(Item item);

    public abstract void ActualizeViewOfSlot();
}
