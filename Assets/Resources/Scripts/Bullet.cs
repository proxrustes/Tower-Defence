using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    Vector3 target_pos;

    public override void Initialize(GameObject target, float speed, Damage damage)
    {
        base.Initialize(target, speed, damage);

        Update();
    }

    private void Update()
    {
        if (target != null)
        {
            target_pos = target.transform.position;
        }

        Vector2 direction_to_target = target_pos - transform.position;

        if (direction_to_target.magnitude <= speed * Time.deltaTime)
        {
            if (target != null) OnHit(target);

            Destroy(gameObject);
            return;
        }

        Vector3 new_pos = transform.position + (Vector3)Vector2.ClampMagnitude(direction_to_target, speed * Time.deltaTime);

        transform.position = new_pos;
    }
}
