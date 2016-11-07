using System;
using System.Collections.Generic;
using UnityEngine;

namespace TileMap
{
    [Serializable]
    public enum TilesTypes
    {
        Nothing = 0,
        Wall = 1,
        Path = 2
    };

    public class TileMap : MonoBehaviour
    {
        //public List<TilesTypes> _keys = new List<TilesTypes>();
        public List<Sprite> _values = new List<Sprite>();
        public List<TilesTypes> _keys = new List<TilesTypes>();
        protected Vector2 ScreenSize;
        private TilesTypes[,] _map;
        protected Dictionary<TilesTypes, Sprite> _tiles = new Dictionary<TilesTypes, Sprite>();
        protected List<GameObject> drawableTiles;
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }

        void Awake()
        {
            for (int a = 0; a < Mathf.Min(_values.Count, _keys.Count); a++)
            {
                _tiles.Add(_keys[a], _values[a]);
            }
        }

        public void SetMapSize(int x, int y)
        {
            _map = new TilesTypes[x, y];
            drawableTiles = new List<GameObject>();
            ScreenSize = GetCameraSizeInWorldUnits() + new Vector2(5,4);
            for(int a = 0; a < (ScreenSize.x-1) * (ScreenSize.y); a++){
                var go = new GameObject();
                go.AddComponent<SpriteRenderer>();
                //go.AddComponent<BoxCollider2D>();
                drawableTiles.Add(go);
            }
            ScreenSize = ScreenSize * 0.5f;
        }

        public bool AddTile(TilesTypes type, Sprite newTile)
        {
            if (_tiles.ContainsKey(type))
            {
                _tiles[type] = newTile;
                return false;
            }
            _tiles.Add(type, newTile);
            return true;
        }

        public void SetMapTile(int x, int y, TilesTypes value)
        {
            if (IsInRange(x, y))
            {
                _map[x, y] = value;
            }
        }

        public TilesTypes GetMapTile(int x, int y)
        {
            try
            {
                if (IsInRange(x, y))
                    return _map[x, y];
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log(_map.GetLength(0).ToString() + "  " + _map.GetLength(1).ToString());
                Debug.Log("X: " + x.ToString() + "  Y: " + y.ToString());
            }
            return TilesTypes.Nothing;
        }

        bool IsInRange(int x, int y)
        {
            if (x < 0 || x >= _map.GetLength(0) || y < 0 || y >= _map.GetLength(1))
                return false;

            return true;
        }

        Vector2 GetCameraSizeInWorldUnits()
        {
            if (Camera.main.orthographic)
            {
                var tmp = Camera.main.ScreenToWorldPoint(Vector3.zero);
                var tmp1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
                tmp = tmp - tmp1;
                return new Vector2(Mathf.Abs(tmp.x), Mathf.Abs(tmp.y));
            }
            return Vector2.zero;
        }

        public void DrawMap()
        {
            var pos = transform.position;
            int count = 0;
            for (int x = Mathf.RoundToInt(pos.x - ScreenSize.x + 1); x < Mathf.RoundToInt(pos.x + ScreenSize.x); x++)
            {
                for (int y = Mathf.RoundToInt(pos.y - ScreenSize.y + 1); y < Mathf.RoundToInt(pos.y + ScreenSize.y); y++, count++)
                {
                    if (count >= drawableTiles.Count) { break;  }
                    drawableTiles[count].transform.position = new Vector3(x, y, -10);
                    drawableTiles[count].GetComponent<SpriteRenderer>().sprite = _tiles[GetMapTile(x, y)];
                }
            }
        }

        public void DrawMap(Vector3 pos)
        {
            int count = 0;
            for (int x = Mathf.RoundToInt(pos.x - ScreenSize.x + 1); x < Mathf.RoundToInt(pos.x + ScreenSize.x); x++)
            {
                for (int y = Mathf.RoundToInt(pos.y - ScreenSize.y + 1); y < Mathf.RoundToInt(pos.y + ScreenSize.y); y++, count++)
                {
                    if (count >= drawableTiles.Count) { break; }
                    drawableTiles[count].transform.position = new Vector3(x, y, -10);
                    drawableTiles[count].GetComponent<SpriteRenderer>().sprite = _tiles[GetMapTile(x, y)];
                }
            }
        }

    }
}
