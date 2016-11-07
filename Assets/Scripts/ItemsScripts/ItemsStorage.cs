using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;
using System;
public class ItemsStorage {

    private static ItemsStorage _instance;

    List<Item> _weapons;
    List<Item> _armors;
    List<Item> _resources;
    List<Item> _potions;

    private ItemsStorage(){
        _weapons = new List<Item>();
        _armors = new List<Item>();
        _resources = new List<Item>();
        _potions = new List<Item>();
    }

    public bool AddNewItem(Item item)
    {
        bool result = false;
        if(item!=null)
        switch (item.ItemType)
        {
            case ItemTypes.Error:
                break;
            case ItemTypes.Weapon:
                _weapons.Add(item);
                result = true;
                break;
            case ItemTypes.Armor:
                _armors.Add(item);
                result = true;
                break;
            case ItemTypes.Resource:
                _resources.Add(item);
                result = true;
                break;
            case ItemTypes.Potion:
                _potions.Add(item);
                result = true;
                break;
            default:
                break;
        }

        return result;
    }

    public Item GetItem(ItemTypes type, int ID) 
    {
        switch (type)
        {
            case ItemTypes.Error:
                break;
            case ItemTypes.Weapon:
                return _weapons.Find(x => x.ItemID == ID);
            case ItemTypes.Armor:
                return _armors.Find(x => x.ItemID == ID);
            case ItemTypes.Resource:
                return new Resource((Resource)_resources.Find(x => x.ItemID == ID));
            case ItemTypes.Potion:
                return new Potion((Potion)_potions.Find(x => x.ItemID == ID));
            default:
                break;
        }
        return null;
    }

    public static ItemsStorage getInstace()
    {
        if (_instance == null)
        {
            _instance = new ItemsStorage();
        }
        return _instance;
    }

}
