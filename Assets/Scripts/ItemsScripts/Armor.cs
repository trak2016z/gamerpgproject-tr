using System;
using System.Collections.Generic;
using Bonuses;

namespace Items
{
    public class Armor : Item, IArmor
    {
        public ArmorTypes ArmorType { get; set; }

        private List<Bonus> _bonuses;
        public List<Bonus> GetBonuses()
        {
            return _bonuses;
        }
        public bool AddBonus(Bonus bonus)
        {
            if (bonus != null)
            {
                _bonuses.Add(bonus);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Armor()
        {
            _bonuses = new List<Bonus>();
        }

        public Armor(Armor item)
        {
            _bonuses = new List<Bonus>();
            CopyData(item);
            this.ArmorType = item.ArmorType;
            foreach (var bns in item.GetBonuses())
            {
                AddBonus(bns);
            }
        }

    }
}
