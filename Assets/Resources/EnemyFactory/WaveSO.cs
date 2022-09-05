using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "wave", order = 1)]
public class WaveSO : ScriptableObject
{
    [System.Serializable]
    
    public class WaveEnemyBatch
    {
        public EnemySO enemy;
        public uint count;
        public uint spacing;
        public int start;
        public int end;
    }

    public List<WaveEnemyBatch> enemies;
   
}
