using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "waves script", order = 1)]
public class WavesScriptSO : ScriptableObject
{
    [Serializable]
    public class WaveScript
    {
        public WaveSO wave;
        public float delay;
    }
    [SerializeField]

    public List<WaveScript> waves;
    

    public IEnumerator Spawner() // wave control
    {
        
        float begin_time = Time.time;
        foreach (WaveScript wave_script in waves)
        {
            yield return new WaitForSeconds(Mathf.Max(0, wave_script.delay - (Time.time - begin_time)));
            Manager.wave_count++;

            foreach (WaveSO.WaveEnemyBatch enemy_batch in wave_script.wave.enemies)
            {
                for (int i = 0; i < enemy_batch.count; i++)
                {
                    EnemyManager.SpawnEnemy(enemy_batch.enemy.prefab, EnemyManager.start_points[enemy_batch.start], EnemyManager.end_points[enemy_batch.end]);
                    if (i != enemy_batch.count - 1)
                    {
                        yield return new WaitForSeconds(enemy_batch.spacing == 0 ? enemy_batch.enemy.std_spacing : enemy_batch.spacing);
                    }
                }
            }
        }
    }
}


