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


    public override bool CanItemBeAdded(Item item)
    {
        if (item.ItemType == ItemTypes.Weapon)
        {
            return true;
        }
        return false;
    }

    public override void AddItem(Item item)
    {
        var tmp = item as IWeapon;
        if (tmp != null)
        {
            _weapon = tmp;
        }
    }
    public override void ActualizeViewOfSlot()
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
}
