using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "tower so", order = 0)]
public class TowerSO : ScriptableObject
{
    [System.Serializable]
    public class MenuItems
    {
        public TowerSO towerSO;
        public uint cost;
    }

    public Sprite icon;
    public GameObject prefab;
    public GameObject projectile_prefab;
    public List<MenuItems> menu_items;
    public uint cost;
    public float range;
    [Range(0.2f, 8)]
    public float fire_rate;
    public Damage damage;
}