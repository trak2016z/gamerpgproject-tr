using UnityEngine;
using System.Collections;
using Items;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IUIPickableComponent
{

    public Item _item { get; set; }
    public int _slotCount { get; set; }

    public Image _itemImage;
    public Text _itemCountText;
    // Use this for initialization
    void Awake()
    {
        _item = null;
        _slotCount = -1;
    }

    public void ActualizeViewOfField()
    {
        if(_itemImage!=null && this._itemCountText !=null){
            if (_item != null)
            {
                _itemImage.sprite = _item.ItemSprite;
                if (_slotCount <= 1)
                {
                    this._itemCountText.text = "";
                }
                else
                {
                    this._itemCountText.text = _slotCount.ToString("##");

                }
                this._itemImage.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                this._itemImage.color = new Color(1f, 1f, 1f, 0f);
                this._itemCountText.text = "";
            }
        }

    }

    public PickableObject getPickableObject(){
        PickableObject tmp = new PickableObject();
        tmp.pickableObject = _item;
        tmp.count = _slotCount;
        _item = null;
        _slotCount = -1;
        return tmp;
    }
    public bool setPickableObject(PickableObject obj){
        if (obj.pickableObject is Item)
        {
            _item = obj.pickableObject as Item;
            _slotCount = obj.count;
            return true;
        }
        return false;
    }

}
