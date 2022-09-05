using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject place_prefab;
    public int lives = 5;
    public LevelSO levelSO;
    public static int wave_count = 0;
    public Text text;
    private void Awake()
    {
        Singleton.AddObject(this);
        Singleton.AddObject(GameObject.FindGameObjectWithTag("map").GetComponent<Grid>());
        Singleton.AddObject("grid", GameObject.FindGameObjectWithTag("map").GetComponent<Grid>());
        Singleton.AddObject("canvas", GameObject.FindGameObjectWithTag("canvas"));
        Singleton.AddObject("place prefab", Resources.Load<GameObject>("Towers/Tower types/Empty place/Empty place"));
        Singleton.AddObject("menu prefab", Resources.Load<GameObject>("Prefabs/Tower menu"));
        Singleton.AddObject("create tower button", Resources.Load<GameObject>("Prefabs/Buttons/create tower"));
        Singleton.AddObject("sell tower button", Resources.Load<GameObject>("Prefabs/Buttons/sell tower"));
        
        SpawnPlaces();

        Money.text = GameObject.Find("balance").GetComponent<Text>();
        text = GameObject.Find("wave_count").GetComponent<Text>();
        
       
        Money.Deposit(levelSO.starterMoney);
        EnemyManager.Initialize(levelSO.wavesScript);
    }

    private void Update()
    {
        text.text = wave_count.ToString();
    }

    private void SpawnPlaces()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Tile tile = (Tile)tilemap.GetTile(pos);

            if (tile != null && tile.sprite.name == "inside_290")
            {
                EmptyPlaceScript.Spawn(tilemap.CellToWorld(pos) + tilemap.cellSize / 2);
            }
        }
    }

    public void EnemyFinishedPath()
    {
        lives--;

        if (lives == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("end game");
    }
}
