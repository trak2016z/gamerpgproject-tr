using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    public class Statistics
    {
        public int ID { get; set; }
        public string StatisticsName { get; set; }
        public float MaximalValue { get; set; }
        public float ActualValue { get; set; }
        public float BaseValue { get; set; }
        public string Desctiption { get; set; }
        public bool Incrementable { get; set; }
        public string DisplayName { get; set; }
        public int IncrementCost { get; set; }
        public bool Override { get; set; }
        public bool IsStatic { get; set; }

        private List<Dependence> _dependencies;

        public bool HasDependencies()
        {
            if (_dependencies.Count > 0) { return true; }
            else
            {
                return false;
            }
        }
        public Statistics()
        {
            _dependencies = new List<Dependence>();
        }

        public void AddDependence(Dependence dependence)
        {
            _dependencies.Add(dependence);
        }

        public List<Dependence> getDependences()
        {
            return _dependencies;
        }
    }
}
