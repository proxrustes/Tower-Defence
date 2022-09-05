using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    Vector2 start_pos, apex, target_pos;

    float travel_time;
    float start_time;

    public override void Initialize(GameObject target, float speed, Damage damage)
    {
        base.Initialize(target, speed, damage);

        start_pos = transform.position; 

        start_time = Time.realtimeSinceStartup;

        apex = ((Vector2)start_pos + (Vector2)target.transform.position) / 2;
        apex.y += 2.5f + ((Vector2)start_pos - (Vector2)target.transform.position).magnitude / 4;

        travel_time = BezierLen(start_pos, apex, target.transform.position) / speed;
        travel_time = Mathf.Max(0.4f, Mathf.Min(2.5f, travel_time));

        Update();
    }

    private void FixedUpdate()
    {
        Vector3 new_apex = (start_pos + target_pos) / 2;
        new_apex.y += 2.5f + (start_pos - target_pos).magnitude / 4;

        apex = Vector3.Lerp(apex, new_apex, 0.05f);
    }

    private void Update()
    {
        float t = (Time.realtimeSinceStartup - start_time) / travel_time;

        if (t >= 1)
        {
            if (target != null) OnHit(target);

            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            target_pos = target.transform.position;
        }

        float angle;
        Vector3 pos = B(t, start_pos, apex, target_pos, out angle);
        pos.z = -0.1f;

        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);

        transform.position = pos;
    }

    Vector2 B(float t, Vector2 p0, Vector2 p1, Vector2 p2, out float angle)
    {
        Vector2 q0 = Vector2.Lerp(p0, p1, t);
        Vector2 q1 = Vector2.Lerp(p1, p2, t);

        angle = Mathf.Atan2(q1.y - q0.y, q1.x - q0.x);

        return Vector2.Lerp(q0, q1, t);
    }

    private float BezierLen(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        Vector2 B = p1 - p0;
        Vector2 F = p2 - p0;
        Vector2 A = F - B;

        float mod_A = A.magnitude;
        float mod_B = B.magnitude;
        float mod_F = F.magnitude;
        float mod_A_sq = mod_A * mod_A;
        float mod_B_sq = mod_B * mod_B;
        float AF = (A.x * F.x + A.y * F.y);
        float AB = (A.x * B.x + A.y * B.y);

        float L = (mod_F * AF - mod_B * AB) / mod_A_sq +
        (mod_B_sq / mod_A + (AB * AB) / (mod_A * mod_A_sq)) *
        (Mathf.Log(mod_A * mod_F + AF) - Mathf.Log(mod_A * mod_B + AB));

        return L;
    }
}
