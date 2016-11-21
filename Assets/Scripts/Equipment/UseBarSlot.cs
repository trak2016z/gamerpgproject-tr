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

    public void UseItem(){
        if(_item != null){

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
    public void setPickableObject(PickableObject obj)
    {
        if (obj.pickableObject is IUsable)
        {
            Debug.Log("Jest Usable");
            _item = obj.pickableObject as IPickable;
            _slotCount = obj.count;
        }
        else
        {
            Debug.Log("NIE Jest Usable");
        }
    }

}
