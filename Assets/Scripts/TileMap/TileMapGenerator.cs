using System;
using System.Collections.Generic;
using UnityEngine;
using CelluarAutomata;
//using System.IO;

namespace TileMap
{
    class TileMapGenerator
    {
        public static bool GenerateTileMapFromMap(Map map, ref TileMap tileMap)
        {
            tileMap.SetMapSize(Mathf.FloorToInt(map.size.x), Mathf.FloorToInt(map.size.y));
            for(int x=0; x < Mathf.FloorToInt(map.size.x); x++)
                for (int y = 0; y < Mathf.FloorToInt(map.size.y); y++)
                {
                    tileMap.SetMapTile(x, y, (TilesTypes)( (int)map.GetFieldType(x,y)));
                }
            tileMap.StartPosition = map.StartPointPosition;
            tileMap.EndPosition = map.EndPointPosition;
            //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", map.texture.EncodeToPNG());
            return true;
        }
    }
}
