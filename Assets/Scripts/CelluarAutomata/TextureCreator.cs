using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CelluarAutomata
{
    public class TextureCreator
    {
        public static void GenerateRandomMap(ref Map map, float randomChance, int borderSize)
        {
            if (map == null) return;

            randomChance = Mathf.Clamp(randomChance, 0f, 1f);
            for (int x = 0; x < map.size.x; x++)
                for (int y = 0; y < map.size.y; y++)
                {
                    if (IsInBorder(borderSize, x, (int)map.size.x) || IsInBorder(borderSize, y, (int)map.size.y))
                    {
                        map.SetFieldType(x, y, FieldTypes.Nothing);
                        continue;
                    }

                    if (Random.value < randomChance)
                    {
                        map.SetFieldType(x, y, FieldTypes.Path);
                    }
                    else
                    {
                        map.SetFieldType(x, y, FieldTypes.Nothing);
                    }
                }
            map.bordersSize = borderSize;
        }

        public static void GenerateRandomMap(ref Map map, float randomChance, int borderSize, Vector2 size)
        {
            map = new Map(size);
            GenerateRandomMap(ref map, randomChance, borderSize);
        }

        static bool IsInBorder(int borderSize, int value, int maxValue)
        {
            if (value < borderSize || value > maxValue - borderSize - 1)
            {
                return true;
            }
            return false;
        }

        public static Sprite CreateSpriteFromTexture(Texture2D texture)
        {
            if (texture != null)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            return null;
        }

        public static void GenerateTextureFromMap(ref Map map)
        {
            if (map.Fields != null)
            {
                var texture = new Texture2D((int)map.size.x, (int)map.size.y);
                for (int x = 0; x < map.size.x; x++)
                    for (int y = 0; y < map.size.y; y++)
                    {
                        texture.SetPixel(x, y, map.Fields[x, y].colour);
                    }

                texture.Apply();
                map.texture = texture;
            }
        }

        public static void GenerateSteps(ref Map map, int steps)
        {
            for (int a = 0; a < steps; a++)
            {
                GenerateOneStep(ref map);
            }
        }

        public static void GenerateOneStep(ref Map map)
        {
            FieldTypes[,] newFieldTypes = new FieldTypes[(int)map.size.x, (int)map.size.y];
            foreach (var item in map.Fields)
            {
                int liveCells = 0;

                for (int x = (int)item.position.x - 1; x <= (int)item.position.x + 1; x++)
                    for (int y = (int)item.position.y - 1; y <= (int)item.position.y + 1; y++)
                    {
                        if (x == item.position.x && y == item.position.y)
                        {
                            continue;
                        }
                        else
                        {
                            if (map.GetFieldType(x, y) == FieldTypes.Path)
                                liveCells++;
                        }

                    }
                newFieldTypes[(int)item.position.x, (int)item.position.y] = getNewType(item.fieldType, liveCells);
            }

            for (int x = 0; x < map.size.x; x++)
            {
                for (int y = 0; y < map.size.y; y++)
                {
                    map.SetFieldType(x, y, newFieldTypes[x, y]);
                }
            }
        }

        public static IEnumerator GenerateOneStep(Map map, Image image, int maxSteps)
        {
            FieldTypes[,] newFieldTypes = new FieldTypes[(int)map.size.x, (int)map.size.y];
            foreach (var item in map.Fields)
            {
                int liveCells = 0;

                for (int x = (int)item.position.x - 1; x <= (int)item.position.x + 1; x++)
                    for (int y = (int)item.position.y - 1; y <= (int)item.position.y + 1; y++)
                    {
                        if (x == item.position.x && y == item.position.y)
                        {
                            continue;
                        }
                        else
                        {
                            if (map.GetFieldType(x, y) == FieldTypes.Path)
                                liveCells++;
                        }

                    }
                newFieldTypes[(int)item.position.x, (int)item.position.y] = getNewType(item.fieldType, liveCells);
            }
            int counter = maxSteps;
            for (int x = 0; x < map.size.x; x++)
            {
                for (int y = 0; y < map.size.y; y++)
                {
                    map.SetFieldType(x, y, newFieldTypes[x, y]);
                    counter--;
                    if (counter == 0)
                    {
                        counter = maxSteps;
                        GenerateTextureFromMap(ref map);
                        image.sprite = CreateSpriteFromTexture(map.texture);
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }


        static FieldTypes getNewType(FieldTypes type, int liveCells)
        {
            if (type == FieldTypes.Path)
            {
                if (liveCells < 2) return FieldTypes.Nothing;
                if (liveCells > 5) return FieldTypes.Nothing;
                return FieldTypes.Path;
            }
            else if (type == FieldTypes.Nothing)
            {
                if (liveCells > 3) return FieldTypes.Path;
            }
            return FieldTypes.Nothing;
        }

        public static void CreateBorders(ref Map map)
        {
            for (int x = 0; x < (int)map.size.x; x++)
            {
                for (int y = 0; y < (int)map.size.y; y++)
                {
                    if (map.GetFieldType(x, y) == FieldTypes.Nothing)
                    {
                        if (IsBorder(x, y, ref map))
                        {
                            map.SetFieldType(x, y, FieldTypes.Wall);
                        }
                    }
                }
            }
        }

        public static IEnumerator CreateBorders(Map map, Image image, int maxSteps)
        {
            int counter = maxSteps;
            for (int x = 0; x < (int)map.size.x; x++)
            {
                for (int y = 0; y < (int)map.size.y; y++)
                {
                    if (map.GetFieldType(x, y) == FieldTypes.Nothing)
                    {
                        if (IsBorder(x, y, ref map))
                        {
                            map.SetFieldType(x, y, FieldTypes.Wall);
                        }
                    }
                    counter--;
                    if (counter == 0)
                    {
                        counter = maxSteps;
                        GenerateTextureFromMap(ref map);
                        image.sprite = CreateSpriteFromTexture(map.texture);
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
            GenerateTextureFromMap(ref map);
            image.sprite = CreateSpriteFromTexture(map.texture);
        }

        static bool IsBorder(int startX, int startY, ref Map map)
        {
            for (int x = startX - 1; x < startX + 2; x++)
            {
                for (int y = startY - 1; y < startY + 2; y++)
                {
                    if (map.GetFieldType(x, y) == FieldTypes.Path)
                        return true;
                }
            }
            return false;
        }

        public static void FillCavern(Map map, Vector2 startPoint)
        {
            int cavernSize = 0;
            Color color = map.cavernColor;
            List<Vector2> nextPointsTooLook = new List<Vector2>();
            nextPointsTooLook.Add(startPoint);
            while (true)
            {
                if (map.GetFieldType((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x - 1, nextPointsTooLook[0].y));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x + 1, nextPointsTooLook[0].y));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x, nextPointsTooLook[0].y - 1));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x, nextPointsTooLook[0].y + 1));
                    cavernSize++;
                }
                nextPointsTooLook.RemoveAt(0);

                if (nextPointsTooLook.Count == 0)
                {
                    map.cavernSize = cavernSize;
                    break;
                }
            }
        }

        public static IEnumerator FillCavern(Map map, Vector2 startPoint, Image image)
        {
            int cavernSize = 0;
            Color color = map.cavernColor;
            int counter = 20;
            List<Vector2> nextPointsTooLook = new List<Vector2>();
            nextPointsTooLook.Add(startPoint);
            while (true)
            {
                //map.SetColor(nextPointsTooLook[0], color);
                if (map.GetFieldType((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x - 1, (int)nextPointsTooLook[0].y, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x - 1, nextPointsTooLook[0].y));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x + 1, (int)nextPointsTooLook[0].y, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x + 1, nextPointsTooLook[0].y));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y - 1, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x, nextPointsTooLook[0].y - 1));
                    cavernSize++;
                }
                if (map.GetFieldType((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1) == FieldTypes.Path && map.GetFieldColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1) == Color.white)
                {
                    map.SetColor((int)nextPointsTooLook[0].x, (int)nextPointsTooLook[0].y + 1, color);
                    nextPointsTooLook.Add(new Vector2(nextPointsTooLook[0].x, nextPointsTooLook[0].y + 1));
                    cavernSize++;
                }
                nextPointsTooLook.RemoveAt(0);
                if (nextPointsTooLook.Count == 0)
                {
                    GenerateTextureFromMap(ref map);
                    image.sprite = CreateSpriteFromTexture(map.texture);
                    map.cavernSize = cavernSize;
                    break;
                }
                counter--;
                if (counter == 0)
                {
                    counter = 20;
                    GenerateTextureFromMap(ref map);
                    image.sprite = CreateSpriteFromTexture(map.texture);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        public static void ClearFromUnusedCaves(Map map)
        {
            foreach (var item in map.Fields)
            {
                if (item.fieldType == FieldTypes.Path && item.colour != map.cavernColor)
                {
                    map.SetFieldType((int)item.position.x, (int)item.position.y, FieldTypes.Nothing);
                }
            }
        }


        public static void CreateMap(ref Map map, int MinMapSize)
        {
            int a = 10;
            int baseMapSize = Mathf.FloorToInt(Mathf.Sqrt(MinMapSize * 2)) * 2;
            Debug.Log(baseMapSize);
            var tmpMap = new Map(new Vector2(baseMapSize, baseMapSize));
            tmpMap.bordersColor = map.bordersColor;
            tmpMap.cavernColor = map.cavernColor;
            while (a > 0)
            {
                GenerateRandomMap(ref tmpMap, 0.4f, 3);
                GenerateSteps(ref tmpMap, 7);
                FillCavern(tmpMap, FindStartPoint(tmpMap));
                if (tmpMap.cavernSize > MinMapSize)
                {
                    ClearFromUnusedCaves(tmpMap);
                    CreateBorders(ref tmpMap);
                    Debug.Log(tmpMap.cavernSize);
                    break;
                }
                a--;
            }
            //MinimalizeSizeOfMap(map);
            map = tmpMap;
            GenerateTextureFromMap(ref map);
            map.FindStartAndEndPointsPosition();
            if (map.cavernSize < MinMapSize)
                Debug.Log("Map is too smal");
        }

        public static Vector2 FindStartPoint(Map map)
        {
            foreach (var item in map.Fields)
            {
                if (item.fieldType == FieldTypes.Path)
                {
                    return item.position;
                }
            }
            return Vector2.zero;
        }

        public static void MinimalizeSizeOfMap(Map map)
        {
            for(int x = map.bordersSize+1; x < Mathf.FloorToInt(map.size.x - map.bordersSize); x++ )
                for (int y = map.bordersSize; y < Mathf.FloorToInt(map.size.y); y++)
                {
                    if (map.GetFieldType(x,y) == FieldTypes.Wall) { continue; }

                    if (y == Mathf.FloorToInt(map.size.y - map.bordersSize))
                    {
                        map.Resize(x + 2, Mathf.FloorToInt(map.size.y));
                        return;
                    }
                }
        }

    }

}