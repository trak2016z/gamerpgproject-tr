using UnityEngine;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Bonuses;

namespace Items
{
    public class LoadItems
    {

        public static void LoadSpritesFromFile(string DirectoryName)
        {
            var sprites = Resources.LoadAll<Sprite>(DirectoryName);
            foreach (var sprite in sprites)
            {
                SpritesContainer.GetInstance().AddElement(sprite.name, sprite);
            }
        }

        public static void LoadItemsFromFile(string FileName)
        {
            var xmlText = Resources.Load<TextAsset>(FileName).text;
            XDocument XmlData = XDocument.Parse(xmlText);
            var Items =
                from el in XmlData.Element("Items").Elements("Item")
                select el;
            foreach (var item in Items)
            {
                LoadItem(item);
            }
        }

        static void LoadItem(XElement data)
        {
            switch (getEnumType<ItemTypes>(data.Attribute("type").Value))
            {
                case ItemTypes.Error:
                    break;
                case ItemTypes.Weapon:
                    {
                        Debug.Log("LoadWeapon");
                        var item = LoadWeapon(data);
                        if (item != null)
                            ItemsStorage.getInstace().AddNewItem(item);
                    }
                    break;
                case ItemTypes.Armor:
                    {
                        Debug.Log("LoadArmor");
                        var item = LoadArmor(data);
                        if (item != null)
                            ItemsStorage.getInstace().AddNewItem(item);
                    }
                    break;
                case ItemTypes.Resource:
                    {
                        Debug.Log("LoadResource");
                        var item = LoadResource(data);
                        if (item != null)
                            ItemsStorage.getInstace().AddNewItem(item);
                    }
                    break;
                case ItemTypes.Potion:
                    {
                        Debug.Log("LoadPotion");
                        var item = LoadPotion(data);
                        if (item != null)
                            ItemsStorage.getInstace().AddNewItem(item);
                    }
                    break;
                default:
                    break;
            }
        }

        static bool LoadItemData<T>(XElement data, ref T item) where T : Item
        {
            if (item == null) { return false; }
            try
            {
                var type = getEnumType<ItemTypes>(data.Attribute("type").Value);
                if (type != default(ItemTypes))
                {
                    item.ItemType = type;
                }
                item.ItemMass = float.Parse(data.Attribute("mass").Value);
                item.ItemSprite = SpritesContainer.GetInstance().GetSprite(data.Attribute("icon").Value);
                item.MaxStackSize = int.Parse(data.Attribute("stack").Value);
                item.ItemName = data.Attribute("name").Value;
                item.ItemID = int.Parse(data.Attribute("ID").Value);
                return true;
            }
            catch
            {
                Debug.Log("Problem with Item Data Load");
                return false;
            }
        }

        static Item LoadWeapon(XElement item)
        {
            try
            {
                Weapon weapon = new Weapon();
                var type = getEnumType<WeaponTypes>(item.Element("WeaponType").Value);
                var minDamage = int.Parse(item.Element("MinimalDamage").Value);
                var maxDamage = int.Parse(item.Element("MaximalDamage").Value);
                var range = float.Parse(item.Element("Range").Value);
                if (type != default(WeaponTypes))
                {
                    weapon.WeaponType = type;
                    weapon.MinimalDamage = minDamage;
                    weapon.MaximalDamage = maxDamage;
                    weapon.Range = range;
                    LoadItemData(item, ref weapon);
                }
                return weapon;
            }
            catch
            {
                Debug.Log("Problem With weapon Load");
                return null;
            }

        }
        static Item LoadArmor(XElement item)
        {
            try
            {
                var armor = new Armor();
                var type = getEnumType<ArmorTypes>(item.Element("ArmorType").Value);
                if (type != default(ArmorTypes))
                {
                    armor.ArmorType = type;
                    var bonuses = item.Elements("Bonus");
                    foreach(var bonus in bonuses){
                        var bonusToAdd = new Bonus();
                        bonusToAdd.statisticType = bonus.Attribute("BonusType").Value;
                        bonusToAdd.value = float.Parse(bonus.Attribute("Value").Value);
                        armor.AddBonus(bonusToAdd);
                    }
                    LoadItemData(item, ref armor);
                }
                return armor;
            }
            catch
            {
                Debug.Log("Problem With Armor Load");
                return null;
            }
        }
        static Item LoadResource(XElement item)
        {
            try
            {
                var resource = new Resource();
                resource.ResourceType = item.Element("ResourceType").Value;
                LoadItemData(item, ref resource);
                return resource;
            }
            catch
            {
                Debug.Log("Problem With Resource Load");
                return null;
            }

        }
        static Item LoadPotion(XElement item)
        {
            try
            {
                var potion = new Potion();
                potion.Statistics = int.Parse(item.Element("StatisticsID").Value);
                potion.BeginValue = float.Parse(item.Element("BeginValue").Value);
                potion.OverTimeValue = float.Parse(item.Element("OverTimeValue").Value);
                potion.Duration = float.Parse(item.Element("Duration").Value);
                LoadItemData(item, ref potion);
                return potion;
            }
            catch
            {
                Debug.Log("Problem With Resource Load");
                return null;
            }
        }

        static T getEnumType<T>(string value) where T : IComparable, IFormattable, IConvertible
        {
            try
            {
                T returnValue = (T)Enum.Parse(typeof(T), value);
                return returnValue;
            }
            catch
            {
                Debug.Log("Problem with enumtype Conversion");
                return default(T);
            }
        }

    }
}