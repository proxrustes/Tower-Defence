using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlaceScript : Place
{
    [SerializeField]
    private List<TowerSO.MenuItems> _menu_items;
    protected override TowerSO b_towerSO { get { return null; } }
    protected override List<TowerSO.MenuItems> menu_items { get { return _menu_items; } }

    public static void Spawn(Vector3 pos)
    {
        GameObject place_obj = Instantiate(Singleton.GetObject<GameObject>("place prefab"), pos, Quaternion.identity, GameObject.Find("Places").transform);
        EmptyPlaceScript place_script = place_obj.GetComponent<EmptyPlaceScript>();
        place_script.GetComponent<BoxCollider2D>().size = Singleton.GetObject<Grid>("grid").cellSize;
    }
}
