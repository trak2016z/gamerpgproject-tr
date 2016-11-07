using UnityEngine;
using System.Collections;

namespace Items
{
    public enum ItemTypes
    {
        Error = 0,
        Weapon,
        Armor,
        Resource,
        Potion

    };

    public enum ArmorTypes
    {
        Error = 0,
        Chest,
        Gloves,
        Shoes,
        Pants
    };

    public enum WeaponTypes
    {
        Error = 0,
        OneHand,
        DoubleHand,
        Bow,
        Staff
    };

    public abstract class Item : IPickable
    {
        public int ItemID { get; set; }
        public ItemTypes ItemType { get; set; }
        public Sprite ItemSprite { get; set; }
        public float ItemMass { get; set; }
        public int MaxStackSize { get; set; }
        public string ItemName { get; set; }
        public bool Usable { get; set; }
        protected void CopyData(Item item)
        {
            ItemID = item.ItemID;
            ItemType = item.ItemType;
            ItemSprite = item.ItemSprite;
            ItemMass = item.ItemMass;
            MaxStackSize = item.MaxStackSize;
            ItemName = item.ItemName;
            Usable = item.Usable;
        }
        public Sprite getImage()
        {
            return ItemSprite;
        }

    }
}