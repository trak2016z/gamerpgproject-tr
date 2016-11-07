using UnityEngine;
namespace CelluarAutomata
{
    public enum FieldTypes
    {
        Nothing = 0,
        Wall = 1,
        Path = 2
    };

    public class Field
    {
        public Color colour { get; set; }
        public FieldTypes fieldType { get; set; }
        public Vector2 position { get; set; }

        public Field()
        {
            colour = Color.black;
            fieldType = FieldTypes.Nothing;
            position = new Vector2();
        }

    }

}