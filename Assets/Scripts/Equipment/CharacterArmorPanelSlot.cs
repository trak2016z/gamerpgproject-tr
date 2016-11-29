using UnityEngine;
using System.Collections;
using Items;
using UnityEngine.UI;
public class CharacterArmorPanelSlot : CharacterPanelItemSlot {

    IArmor _armor;
    public ArmorTypes actualArmorType;
    protected Sprite _orginalSprite;
	// Use this for initialization
	void Awake () {
        _armor = null;
        _orginalSprite = GetComponent<Image>().sprite;
	}

    public override void ActualizeViewOfField()
    {
        if (_armor != null)
        {
            GetComponent<Image>().sprite = (_armor as Item).getImage();
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            GetComponent<Image>().sprite = _orginalSprite;
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        }
    }

    public override  PickableObject getPickableObject()
    {
        var returnObj = new PickableObject();
        returnObj.count = 1;
        if (_armor == null)
        {
            returnObj.count = -1;
        }
        returnObj.pickableObject = _armor as IPickable;
        _armor = null;
        return returnObj;
    }
    public override bool setPickableObject(PickableObject obj)
    {
        var tmp = obj.pickableObject as Armor;
        if (tmp != null && tmp.ArmorType == actualArmorType)
        {
            _armor = tmp;
            return true;
        }

        return false;
    }

}
