using UnityEngine;
using System.Collections;

namespace Character
{
    public class CharacterDataController : MonoBehaviour
    {

        public CharacterStatistics CharacterStatistics { get; set; }
        public string CharacterDataFile {protected set; get;}
        // Use this for initialization
        void Awake()
        {
            CharacterDataFile = "CharacterData";
        }

        // Update is called once per frame
        void Update()
        {

        }

    }

}