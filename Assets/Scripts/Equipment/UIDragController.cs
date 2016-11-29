using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    protected Transform _dragedObjectStartTransform;
    public static bool ShouldBeDroped = true;
    private GameObject _ImageWhenItemDraged;
    private static PickableObject _PickedObject;

    private OnGroundObjectController _onGrounController;
    private Transform _characterPosition;
    void Start()
    {
        _ImageWhenItemDraged = GameObject.Find("DragedItemImage");
        _onGrounController = GameObject.FindObjectOfType<OnGroundObjectController>();
        _characterPosition = GameObject.FindObjectOfType<CharacterControll>().transform;

    }
    // Use this for initialization

    public void OnDrag(PointerEventData eventData)
    {
        //_moveTransform.position = Input.mousePosition;
        _ImageWhenItemDraged.transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var baseComponent = GetComponent<IUIPickableComponent>();
        var pickedObj = baseComponent.getPickableObject();
        baseComponent.ActualizeViewOfField();
        if (baseComponent != null && pickedObj.pickableObject != null)
        {
            _dragedObjectStartTransform = transform;
            _PickedObject = pickedObj;
            _ImageWhenItemDraged.GetComponent<Image>().sprite = _PickedObject.pickableObject.getImage();
            //_dragedObjectStartTransform.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ShouldBeDroped)
        {
            _onGrounController.AddObjectOnGround(_PickedObject, _characterPosition.position);
        }else if (_PickedObject.pickableObject != null)
        {
            Debug.Log("PickedOBJ: " + _PickedObject.pickableObject.ToString() + " count: " + _PickedObject.count.ToString());
            _dragedObjectStartTransform.GetComponent<IUIPickableComponent>().setPickableObject(getPickedObject());
            _dragedObjectStartTransform.GetComponent<IUIPickableComponent>().ActualizeViewOfField();
        }

        _PickedObject.pickableObject = null;
        _PickedObject.count = -1;
        _dragedObjectStartTransform = null;
        _ImageWhenItemDraged.GetComponent<Image>().sprite = null;
        _ImageWhenItemDraged.transform.position = new Vector3(-1000f, -1000f, 0);
        //_dragedObjectStartTransform.GetComponent<Image>().raycastTarget = true;
        ShouldBeDroped = true;
    }

    public static PickableObject getPickedObject()
    {
        var tmp = _PickedObject;
        _PickedObject.pickableObject = null;
        _PickedObject.count = -1;
        return tmp;
    }

    public static void setPickedObject(PickableObject obj)
    {
        _PickedObject = obj;
    }
}
