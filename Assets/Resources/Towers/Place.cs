using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Place : MonoBehaviour
{
    [SerializeField]
    protected abstract List<TowerSO.MenuItems> menu_items { get; }
    [SerializeField]
    protected abstract TowerSO b_towerSO { get; }

    private BoxCollider2D collider;

    protected void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();

        Vector2 scale = Singleton.GetObject<Grid>().cellSize;
        scale.x /= collider.size.x;
        scale.y /= collider.size.y;

        transform.localScale *= Mathf.Min(scale.x, scale.y);
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && MyPhys.MouseRaycast(collider))
        {
            CreateMenu();
        }
    }


    protected void CreateMenu()
    {
        GameObject menu_obj = Instantiate(Singleton.GetObject<GameObject>("menu prefab"), Singleton.GetObject<GameObject>("canvas").transform);
        TowerMenuScript menu_script = menu_obj.GetComponent<TowerMenuScript>();

        menu_script.Initialize(gameObject, menu_items, b_towerSO);
    }
}
