using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;

namespace Character
{
	public class CharacterDataController : MonoBehaviour
	{

		public CharacterStatistics characterStatistics { get; set; }

		public string CharacterDataFile { protected set; get; }
		// Use this for initialization
		void Awake ()
		{
			CharacterDataFile = "CharacterData";
		}

		// Update is called once per frame
		void Update ()
		{

		}


		public void UsePotion (Potion potion)
		{
			Debug.Log ("Potion Data: " + potion.Statistics.ToString () + "    " + potion.BeginValue.ToString () + "   " + potion.OverTimeValue.ToString ());
			Statistics stat;
			characterStatistics.GetStatistics (potion.Statistics, out stat);
			stat.ActualValue += potion.BeginValue;
			characterStatistics.CalculateDependencies ();
			StartCoroutine (UsePotionCorutine (stat, potion));
		}

		public IEnumerator UsePotionCorutine (Statistics stat, Potion potion)
		{
			float time = potion.Duration;
			while (true) {
				if (time <= 0) {
					break;
				}
				stat.ActualValue = Mathf.Clamp (stat.ActualValue + potion.OverTimeValue, 0, stat.MaximalValue);
				time--;
				yield return new WaitForSeconds (1.0f);
			}
			stat.ActualValue -= potion.BeginValue;
			characterStatistics.CalculateDependencies ();
		}

		public string GetStatisticsName (int statisticsID)
		{
			return characterStatistics.GetStatisticsName (statisticsID);
		}

		public void SetDamage(float value){
			Statistics stat;
			characterStatistics.GetStatistics (6, out stat);
			if (stat == null) {
				return;
			}
			stat.ActualValue = Mathf.Clamp (stat.ActualValue - value, 0, stat.MaximalValue);
			Debug.Log ("Health: " + stat.ActualValue.ToString ());
		}

		public void SetDamage(List<int> values){
			Statistics stat;
			characterStatistics.GetStatistics (6, out stat);
			if (stat == null) {
				return;
			}
			foreach (var value in values) {
				stat.ActualValue = Mathf.Clamp (stat.ActualValue - value, 0, stat.MaximalValue);
			}
		}

	}

}