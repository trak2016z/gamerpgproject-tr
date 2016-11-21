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


    public override bool CanItemBeAdded(Item item)
    {
        if(item.ItemType == ItemTypes.Armor)
        {
            if((item as Armor).ArmorType == actualArmorType){
                return true;
            }
        }

        return false;
    }

    public override void AddItem(Item item)
    {
        var tmp = item as IArmor;
        if (tmp != null && tmp.ArmorType == actualArmorType)
        {
            _armor = tmp;
        }
    }

    public override void ActualizeViewOfSlot()
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
}
