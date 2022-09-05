using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected GameObject target;
    protected Damage damage;
    protected float speed;

    public virtual void Initialize(GameObject target, float speed, Damage damage) 
    {
        this.speed = speed;
        this.target = target;
        this.damage = damage;
    }

    protected void OnHit(GameObject target)
    {
        target.GetComponent<EnemyScript>().TakeDamage(damage);
    }
}
