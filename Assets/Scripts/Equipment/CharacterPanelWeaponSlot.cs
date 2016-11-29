using UnityEngine;
using System.Collections;
using Items;
using UnityEngine.UI;
public class CharacterPanelWeaponSlot : CharacterPanelItemSlot {

    IWeapon _weapon;
    protected WeaponTypes actualArmorType;
    protected Sprite _orginalSprite;
    // Use this for initialization
    void Awake()
    {
        _weapon = null;
        _orginalSprite = GetComponent<Image>().sprite;
    }

    public override void ActualizeViewOfField()
    {
        if (_weapon != null)
        {
            GetComponent<Image>().sprite = (_weapon as Item).getImage();
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            GetComponent<Image>().sprite = _orginalSprite;
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        }
    }
    public override PickableObject getPickableObject()
    {
        var returnObj = new PickableObject();
        returnObj.count = 1;
        if (_weapon == null)
        {
            returnObj.count = -1;
        }
        returnObj.pickableObject = _weapon as IPickable;
        _weapon = null;
        return returnObj;
    }
    public override bool setPickableObject(PickableObject obj)
    {
        var tmp = obj.pickableObject as Weapon;
        if (tmp != null)
        {
            _weapon = tmp;
            return true;
        }

        return false;
    }
}
