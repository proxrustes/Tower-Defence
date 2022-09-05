using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static Vector3[] start_points;
    public static Vector3[] end_points;

    public static WavesScriptSO wave_script;

    public static void Initialize(WavesScriptSO _wave_script)
    {
        wave_script = _wave_script;
    }

    public void Awake()
    {
        List<Vector3> _start_points = new List<Vector3>();
        List<Vector3> _end_points = new List<Vector3>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("start point"))
        {
            _start_points.Add(obj.transform.position);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("end point"))
        {
            _end_points.Add(obj.transform.position);
        }

        start_points = _start_points.ToArray();
        end_points = _end_points.ToArray();
        
        StartCoroutine(wave_script.Spawner());
    }

    public static void SpawnEnemy(GameObject prefab, Vector3 start_point, Vector3 end_points)
    {
        GameObject obj = Instantiate(prefab, start_point, Quaternion.identity);
        EnemyScript enemy_script = obj.GetComponent<EnemyScript>();
        enemy_script.Initialize(Waypoints.GetPath(start_point, end_points));
    }

}