using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using Character;
using UnityEngine;
namespace DataLoader
{
    public class LoadData
    {
        public static CharacterStatistics LoadCharacterData(string fileName)
        {
            try
            {
                var xmlText = Resources.Load<TextAsset>(fileName).text;
                XDocument xmlData = XDocument.Parse(xmlText);
                var StatData = xmlData.Element("Character").Elements();
                var charStat = new CharacterStatistics();
                foreach (var item in StatData)
                {
                    var stat = new Statistics();
                    stat.StatisticsName = item.Name.ToString();
                    Debug.Log(stat.StatisticsName);
                    stat.BaseValue = stat.ActualValue = stat.MaximalValue = GetFloat(item.Element("Value").Value);
                    stat.ID = GetInt(item.Element("StatID"));
                    stat.Desctiption = item.Element("Description").Value;
                    stat.Incrementable = GetBool(item.Element("Incrementable").Value);
                    stat.DisplayName = item.Element("DisplayName").Value;
                    stat.IncrementCost = GetInt(item.Element("IncrementCost").Value);
                    stat.Override = GetBool(item.Element("Override").Value);
                    stat.IsStatic = GetBool(item.Element("Static").Value);
                    foreach (var dep in item.Elements("Dependence"))
                    {
                        var dependence = new Dependence();
                        dependence.StatisticsID = GetInt(dep.Element("DepStatID").Value);
                        dependence.Value = GetFloat(dep.Element("Value").Value);
                        dependence.Function = GetDelegate(dep.Element("Operation").Value);
                        stat.AddDependence(dependence);
                    }
                    charStat.AddStatistics(stat);
                }
                /****************** Rest Data if need ********************/
                charStat.CalculateDependencies();
                return charStat;
            }
            catch
            {
                Debug.Log("Cannot Load CHaracter Data");
                return null;
            }
        }

        private static bool GetBool(string value, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                Debug.Log("Bool Conversiton Error");
                return defaultValue;
            }
        }

        private static float GetFloat(string value, float defaultValue = 0.0f)
        {
            try
            {
                return float.Parse(value);
            }
            catch
            {
                Debug.Log("Float Conversiton Error");
                return defaultValue;
            }
        }

        private static int GetInt(string value, int defaultValue = 0)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                Debug.Log("Integer Conversiton Error");
                return defaultValue;
            }
        }

        private static int GetInt(XElement item, int defaultValue = 0)
        {
            if (item == null)
            {
                return defaultValue;
            }
            return int.Parse(item.Value);
        }

        private static Func<float, float, float> GetDelegate(string name)
        {
            try
            {
                Debug.Log("Delegate: " + name);
                switch (name)
                {
                    case "Mul":
                        return (x, y) =>
                        {
                            float result = 0.0f;
                            for (int a = 0; a < x; a++) { result += a * y; }
                            return result;
                        };
                    case "Div":
                        return (x, y) => x / y;
                    case "Add":
                        return (x, y) => x * y;
                    case "Sub":
                        return (x, y) => -(x * y);
                    default:
                        return new Func<float, float, float>((x, y) => 0.0f);
                }
            }
            catch
            {
                Debug.Log("Cannot Add Dependence");
                return null;
            }
        }


    }
}
