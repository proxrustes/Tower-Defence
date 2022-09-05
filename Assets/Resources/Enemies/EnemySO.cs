using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public float std_spacing;

    public uint bounty;
    public float speed;
    public int hp;

    public bool XY_symmetry;
    public bool X_symmetry;
    public bool Y_symmetry;
}
