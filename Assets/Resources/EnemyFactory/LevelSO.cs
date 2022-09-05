using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public WavesScriptSO wavesScript;
    public uint starterMoney;
}
