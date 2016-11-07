using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using CelluarAutomata;
using TileMap;

public class TextScript : MonoBehaviour {

    protected Image image;
    public Map map;
	void Start () {
        map = new Map(new Vector2(200,100));
        map.bordersColor = Color.magenta;
        map.cavernColor = Color.blue;
        TextureCreator.GenerateRandomMap(ref map, 0.6f, 3);
        TextureCreator.GenerateTextureFromMap(ref map);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TextureCreator.GenerateSteps(ref map, 1);
            //TextureCreator.GenerateTextureFromMap(ref map);
            //image.sprite = TextureCreator.CreateSpriteFromTexture(map.texture);
            StartCoroutine(TextureCreator.GenerateOneStep(map, image, 400));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", map.texture.EncodeToPNG());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(TextureCreator.CreateBorders(map, image, 400));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var item in map.Fields)
            {
                if (item.fieldType == FieldTypes.Path)
                {
                    StartCoroutine(TextureCreator.FillCavern(map, item.position, image));
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TextureCreator.CreateMap(ref map, 2000);
            TextureCreator.GenerateTextureFromMap(ref map);
            var go = GameObject.Find("Character");
            var tmp = go.GetComponent<TileMap.TileMap>();
            TileMapGenerator.GenerateTileMapFromMap(map, ref tmp);
            //image.sprite = TextureCreator.CreateSpriteFromTexture(map.texture);
        }

	}
}
