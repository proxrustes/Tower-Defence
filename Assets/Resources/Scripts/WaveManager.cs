using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveScript
    {
        public WaveSO wave;
        public int delay;
    }
    [SerializeField]

    public List<WaveScript> waves;
}