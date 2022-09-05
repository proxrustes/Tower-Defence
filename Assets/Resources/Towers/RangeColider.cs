using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeColider : MonoBehaviour
{
    public HashSet<GameObject> enemies = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemies.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision.gameObject);
    }
}