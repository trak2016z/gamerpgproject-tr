using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public class Weapon : Item, IWeapon
    {
        public WeaponTypes WeaponType { get; set; }
        public int MinimalDamage{get;set;}
        public int MaximalDamage { get; set; }
        public float Range { get; set; }

        public Weapon()
        {

        }

        public Weapon(Weapon item)
        {
            this.WeaponType = item.WeaponType;
            this.MinimalDamage = item.MinimalDamage;
            this.MaximalDamage = item.MaximalDamage;
            this.Range = item.Range;
            CopyData(item);
        }

    }
}
