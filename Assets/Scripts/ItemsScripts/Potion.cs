using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Character;
using Usable;

namespace Items
{
    public class Potion : Item, IPotion, IUsable
    {
        public int Statistics { get; set; }
        public float BeginValue { get; set; }
        public float OverTimeValue { get; set; }
        public float Duration { get; set; }
        public Potion(Potion item)
        {
            Statistics = item.Statistics;
            BeginValue = item.BeginValue;
            OverTimeValue = item.OverTimeValue;
            Duration = item.Duration;
            CopyData(item);
        }

        public Potion(){

        }
        public void Use()
        {
            GameObject.FindObjectOfType<CharacterDataController>().UsePotion(this);
        }
    }
}
