using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Damage
{
    public enum DamageType
    {
        physical,
        magical
    }

    public DamageType type;
    public int amount;

    public Damage(DamageType type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
}