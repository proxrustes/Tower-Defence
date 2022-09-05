using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private EnemySO SO;
    [SerializeField]
    private int hp;
    private Vector3[] path;
    [SerializeField]
    private uint next_waypoint_i = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    public void Initialize(Vector3[] path)
    {
        rigidbody = GetComponent<Rigidbody2D>();
        this.path = path;
        hp = SO.hp;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FollowPath();
    }

    protected void FollowPath()
    {
        Vector3 delta_path = (Vector2)(path[next_waypoint_i] - transform.position);
        float step_len = SO.speed * Time.deltaTime;

        while (true)
        {
            if (delta_path.magnitude > step_len)
            {
                rigidbody.transform.position += delta_path.normalized * step_len;
                break;
            }
            else
            {
                rigidbody.MovePosition(new Vector3(path[next_waypoint_i].x, path[next_waypoint_i].y, transform.position.z));

                if (next_waypoint_i == path.Length - 1)
                {
                    Destroy(gameObject);
                    OnPathFinish();
                    return;
                }

                step_len -= delta_path.magnitude;
                next_waypoint_i++;
                delta_path = (Vector2)(path[next_waypoint_i] - transform.position);

                Vector3 direction = path[next_waypoint_i] - path[next_waypoint_i - 1];
                int angle = (int)Mathf.Round(Mathf.Atan2(direction.x, direction.y) / (Mathf.PI / 2));

                angle = (4 + angle) % 4 - 1;

                OnTurn(angle);
            }
        }
    }

    public void TakeDamage(Damage damage)
    {
        hp -= damage.amount;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Money.Deposit(SO.bounty);
        Destroy(gameObject);
    }

    private void OnTurn(int dir)
    {
        const int layer = 0;
        float normilized_time = animator.GetCurrentAnimatorStateInfo(layer).normalizedTime % 1;

        if (animator == null)
        {
            return;
        }

        /* dir meaning
        0 - right
        1 - мотя
        2 - left
        3 - up

           3
           |
        2--+--0
           |  
           1

        */

        if (SO.XY_symmetry)
        {
            animator.Play("Universal", layer, normilized_time);
            
            transform.rotation = Quaternion.Euler(0, 0, dir * 90);
        }
        else
        {
            if ((dir & 1) == 1) // y
            {
                spriteRenderer.flipX = false;

                if (SO.Y_symmetry)
                {
                    animator.Play("Y Universal", layer, normilized_time);
                    spriteRenderer.flipY = dir == 3;
                }
                else
                {
                    if (dir == 1)
                    {
                        animator.Play("down", layer, normilized_time);
                    }
                    else
                    {
                        animator.Play("up", layer, normilized_time);
                    }
                }
            }
            else // x
            {
                spriteRenderer.flipY = false;

                if (SO.X_symmetry)
                {
                    animator.Play("X Universal", layer, normilized_time);

                    spriteRenderer.flipX = dir == 2;
                }
                else
                {
                    if (dir == 0)
                    {
                        animator.Play("right", layer, normilized_time);
                    }
                    else
                    {
                        animator.Play("left", layer, normilized_time);
                    }
                }
            }
        }
    }

    protected void OnPathFinish()
    {
        Singleton.GetObject<Manager>().EnemyFinishedPath();
    }
}
