using UnityEngine;
using System.Collections;
using Usable;
using UnityEngine.UI;

public class UseBarSlot : MonoBehaviour, IUIPickableComponent {

    public IPickable _item;
    public int _slotCount;
	// Use this for initialization
    public Image _itemImage;
    public Text _itemCountText;

    void Awake(){
        _item = null;
        _slotCount = -1;
    }

    public void ActualizeViewOfField(){

        if(_item != null){
            _itemImage.sprite = _item.getImage();
            _itemImage.color = new Color(1.0f,1.0f,1.0f,1.0f);
            if(_slotCount > 1){
                _itemCountText.text = _slotCount.ToString();
            }else{
                _itemCountText.text = "";
            }
        }else{
            _itemImage.sprite = null;
            _itemImage.color = new Color(1.0f,1.0f,1.0f,0.0f);
            _itemCountText.text = "";
        }

    }
    public PickableObject getPickableObject()
    {
        PickableObject tmp = new PickableObject();
        tmp.pickableObject = _item;
        tmp.count = _slotCount;
        _item = null;
        _slotCount = -1;
        return tmp;
    }
    public bool setPickableObject(PickableObject obj)
    {
        if (obj.pickableObject is IUsable)
        {
            Debug.Log("Jest Usable");
            _item = obj.pickableObject as IPickable;
            _slotCount = obj.count;
            return true;
        }
        else
        {
            Debug.Log("NIE Jest Usable");
        }
        return false;
    }

    public void UseObject()
    {
        if (_item != null)
        {
            var tmp = _item as IUsable;
            if (tmp != null)
            {
                _slotCount--;
                tmp.Use();

                if (_slotCount <= 0)
                {
                    _slotCount = -1;
                    _item = null;
                }
            }
        }
        ActualizeViewOfField();
    }

}
