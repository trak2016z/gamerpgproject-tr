using UnityEngine;
using System;
namespace TileMap
{
    public class Tile
    {
        public TilesTypes ID { get; set; }
        public Sprite sprite { get; set; }

        public Tile(TilesTypes tilesTypes, Sprite sprite1)
        {
            // TODO: Complete member initialization
            this.ID = tilesTypes;
            this.sprite = sprite1;
        }
    }
}
