using UnityEngine;

namespace CelluarAutomata
{
    public class Map
    {

        public Field[,] Fields;
        public Vector2 size { get; set; }
        public Texture2D texture { get; set; }

        public Color cavernColor { get; set; }
        public Color bordersColor { get; set; }
        public int bordersSize { get; set; }
        public int cavernSize { get; set; }

        public Vector2 StartPointPosition { get; set; }
        public Vector2 EndPointPosition { get; set; }
        public Map(Vector2 mapSize)
        {
            Fields = new Field[(int)mapSize.x, (int)mapSize.y];
            size = mapSize;
            Initialize();
        }
        void Initialize()
        {
            if (Fields != null)
            {
                for (int x = 0; x < Fields.GetLength(0); x++)
                {
                    for (int y = 0; y < Fields.GetLength(1); y++)
                    {
                        Fields[x, y] = new Field();
                        Fields[x, y].position = new Vector2(x, y);
                    }
                }
            }
        }

        public void Resize(int x, int y)
        {
            Field[,] newFields = new Field[x, y];
            for (int a = 0; a < x; a++)
                for (int b = 0; b < y; b++)
                {
                    GetField(a, b, out newFields[a, b]);
                }

            Fields = newFields;
            size = new Vector2(Fields.GetLength(0), Fields.GetLength(1));
        }


        public void SetColor(int x, int y, Color color)
        {
            Fields[x, y].colour = color;
        }
        public void SetColor(Vector2 position, Color color)
        {
            SetColor((int)position.x, (int)position.y, color);
        }

        public void SetFieldType(int x, int y, FieldTypes type)
        {
            Fields[x, y].fieldType = type;
            switch (type)
            {
                case FieldTypes.Nothing:
                    SetColor(x, y, Color.black);
                    break;
                case FieldTypes.Wall:
                    SetColor(x, y, bordersColor);
                    break;
                case FieldTypes.Path:
                    SetColor(x, y, Color.white);
                    break;
                default:
                    break;
            }
        }

        public void GetField(int x, int y, out Field field)
        {
            if (InRange(x, y))
            {
                field = Fields[x, y];
            }
            field = new Field();
        }

        public FieldTypes GetFieldType(int x, int y)
        {
            if (InRange(x, y))
            {
                return Fields[x, y].fieldType;
            }
            return FieldTypes.Nothing;
        }

        public FieldTypes GetFieldType(Vector2 position)
        {
            return GetFieldType((int)position.x, (int)position.y);
        }

        public Color GetFieldColor(int x, int y)
        {
            if (InRange(x, y))
            {
                return Fields[x, y].colour;
            }
            return Color.black;
        }
        public Color GetFieldColor(Vector2 position)
        {
            return GetFieldColor((int)position.x, (int)position.y);
        }
        private bool InRange(int x, int y)
        {
            if (x < 0 || x >= Fields.GetLength(0) || y < 0 || y >= Fields.GetLength(1))
            {
                return false;
            }
            return true;
        }

        public void FindStartAndEndPointsPosition()
        {
            bool startFromBottom = Random.value > 0.5 ? true : false;
            bool startFromLeft = Random.value > 0.5 ? true : false;
            StartPointPosition = GetFirstFieldFrom(startFromBottom, startFromLeft);
            EndPointPosition = GetFirstFieldFrom(!startFromBottom, !startFromLeft);
        }

        public Vector2 GetFirstFieldFrom(bool startFromBottom, bool startFromLeft)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    int xValue = startFromLeft ? x : (int)size.x - x;
                    int yValue = startFromBottom ? y : (int)size.y - y;
                    if (GetFieldType(xValue, yValue) == FieldTypes.Path)
                    {
                        return new Vector2(xValue, yValue);
                    }
                }
            }
            return Vector2.zero;
        }

    }

}