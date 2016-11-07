using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Character
{
    public class Dependence
    {
        public int StatisticsID { get; set; }
        public float Value { get; set; }
        public Func<float, float, float> Function { get; set; }
    }
}
