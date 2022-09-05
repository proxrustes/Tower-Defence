using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTower : MonoBehaviour
{
    public GameObject tower;

    void OnMouseDown()
    {
        Instantiate(tower, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
