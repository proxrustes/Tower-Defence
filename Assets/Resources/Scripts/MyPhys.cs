using UnityEditor;
using UnityEngine;

public static class MyPhys
{
    public static bool MouseRaycast(Collider2D collider)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector3.zero, Mathf.Infinity);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == collider) return true;
        }

        return false;
    }
}
