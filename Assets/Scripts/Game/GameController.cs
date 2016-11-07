using UnityEngine;
using System.Collections;
using TileMap;
using CelluarAutomata;
using DataLoader;
using Character;
using Items;
public class GameController : MonoBehaviour
{

    public Transform _Player;
    protected Vector3 _oldPosition;
    // Use this for initialization
    void Awake()
    {
        CreateMap();
        InitializePlayer();
        LoadPlayerData();

        LoadItems.LoadSpritesFromFile("Items");
        LoadItems.LoadItemsFromFile("ItemsXML");

    }

    void Start()
    {
        var eqController = FindObjectOfType<EquipmentPanelController>();
        for (int a = 0; a < 10; a++)
        {
            var item = ItemsStorage.getInstace().GetItem(ItemTypes.Potion, 4);
            eqController.AddItemToEquipment(item, 10);
        }
        eqController.AddItemToEquipment(ItemsStorage.getInstace().GetItem(ItemTypes.Weapon, 1), 1);
        eqController.AddItemToEquipment(ItemsStorage.getInstace().GetItem(ItemTypes.Weapon, 2), 1);

        eqController.ActualizeViewOfEquipment();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_Player.position.x - _oldPosition.x) > 1 || Mathf.Abs(_Player.position.y - _oldPosition.y) > 1)
        {
            ActualDrawingMap.UpdateMap(_Player.position.x, _Player.position.y);
            _oldPosition = _Player.position;
        }
    }

    protected void InitializePlayer()
    {
        var oldPosition = _Player.position;
        var newPosition = ActualDrawingMap.GetStartPosition();
        _Player.position = new Vector3(newPosition.x, newPosition.y, oldPosition.z);
    }

    protected void CreateMap()
    {
        var map = new Map(new Vector2(200, 100));
        map.bordersColor = Color.magenta;
        map.cavernColor = Color.blue;
        TextureCreator.CreateMap(ref map, 2000);
        //TextureCreator.GenerateTextureFromMap(ref map);  // Tworzy teksture mapy , moze byc wykorzystane przy minimapie
        var tileMap = GetComponent<TileMap.TileMap>();
        TileMapGenerator.GenerateTileMapFromMap(map, ref tileMap);
        ActualDrawingMap.SetActualTileMap(ref tileMap);
        //ActualDrawingMap.UpdateMap(0, 0); // Testowa funkcja rysuje mape na pozycji (0, 0)
    }

    protected void LoadPlayerData()
    {
        var playerDataController = _Player.GetComponent<CharacterDataController>();
        var statistics = DataLoader.LoadData.LoadCharacterData(playerDataController.CharacterDataFile);
        Debug.Log("Character Data Loaded");
        if (statistics != null)
        {
            playerDataController.CharacterStatistics = statistics;
        }
        _Player.GetComponent<CharacterControll>()._moveSpeed = playerDataController.CharacterStatistics.FindStatistics("MovementSpeed").MaximalValue;
        /*********************************** TO DO *********************************/
    }

}
