using System;
using System.Collections.Generic;

namespace Items
{
    public interface IWeapon
    {
        WeaponTypes WeaponType { get; set; }
        int MinimalDamage { get; set; }
        int MaximalDamage { get; set; }
        float Range { get; set; }
    }
}
