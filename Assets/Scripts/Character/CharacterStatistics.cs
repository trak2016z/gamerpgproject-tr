using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    public class CharacterStatistics
    {
        private Dictionary<int, Statistics> _statistics;

        public CharacterStatistics()
        {
            _statistics = new Dictionary<int, Statistics>();
        }

        public Statistics[] GetStatistics()
        {
            return _statistics.Values.ToArray();
        }


        public bool AddStatistics(Statistics statistics)
        {
            try
            {
                _statistics.Add(statistics.ID, statistics);
                return true;
            }
            catch (ArgumentException)
            {
                Debug.Log("Statistics already Exist");
                return false;
            }
            catch
            {
                Debug.Log("Error with adding statistics");
                return false;
            }
        }

        public Statistics FindStatistics(int statisticsID)
        {
            try
            {
                return _statistics[statisticsID];
            }
            catch
            {
                Debug.Log("Statistics dont exist in actual Game");
                return null;
            }
        }

        public Statistics FindStatistics(string StatisticName)
        {
            for (int a = 0; a < _statistics.Count; a++)
            {
                if (_statistics.ElementAt(a).Value.StatisticsName == StatisticName)
                    return _statistics.ElementAt(a).Value;
            }
            return null;
        }

        public void GetStatistics(int statisticsID, out Statistics ObjectToChange)
        {
            try
            {
                if (_statistics.ContainsKey(statisticsID))
                {
                    ObjectToChange = _statistics[statisticsID];
                    return;
                }
                ObjectToChange = null;
            }
            catch(ArgumentException)
            {
                ObjectToChange = null;
            }
        }

        public void CalculateDependencies()
        {
            foreach (var item in _statistics.Keys)
            {
                var stat = _statistics[item];
                if (stat.HasDependencies())
                {
                    var newValue = stat.BaseValue;
                    var deps = stat.getDependences();
                    foreach (var dep in deps)
                    {
                        newValue += dep.Function(FindStatistics(dep.StatisticsID).MaximalValue, dep.Value);
                    }
                    stat.MaximalValue = newValue;
                }

            }

        }

    }
}
