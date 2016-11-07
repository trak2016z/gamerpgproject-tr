using UnityEngine;
using System.Collections;
using TileMap;
public class ActualDrawingMap
{

    static TileMap.TileMap _TileMap;
    // Use this for initialization
    public static void SetActualTileMap(ref TileMap.TileMap tileMap)
    {
        _TileMap = tileMap;
    }

    public static TilesTypes GetTileTypeFromPosition(float x, float y)
    {
        if (_TileMap == null) { return TilesTypes.Nothing; }
        return _TileMap.GetMapTile(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }

    public static TilesTypes GetTileTypeFromPosition(Vector2 position)
    {
        return GetTileTypeFromPosition(position.x, position.y);
    }

    public static void UpdateMap(float x, float y)
    {
        if (_TileMap == null) { return; }
        _TileMap.DrawMap(new Vector3(x, y, 0f));
    }

    public static Vector2 GetStartPosition()
    {
        if (_TileMap == null) { return Vector2.zero; }
        return _TileMap.StartPosition;
    }

    public static Vector2 GetEndPosition()
    {
        if (_TileMap == null) { return Vector2.zero; }
        return _TileMap.EndPosition;
    }

}
