using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Place
{
    protected override TowerSO b_towerSO { get { return towerSO; } }
    public TowerSO towerSO;

    protected override List<TowerSO.MenuItems> menu_items { get { return b_towerSO.menu_items; } }
    
    public HashSet<GameObject> enemies;

    private float next_shot = 0;

    private void Awake()
    {
        base.Awake();
        enemies = GetComponentInChildren<RangeColider>().enemies;
    }

    public void Initialize(TowerSO towerSO)
    {
        this.towerSO = towerSO;
        GetComponentInChildren<CircleCollider2D>().radius = towerSO.range;
    }

    public void Update()
    {
        base.Update();

        GameObject target = ChooseTarget();

        if (target != null && next_shot <= Time.realtimeSinceStartup)
        {
            next_shot = Time.realtimeSinceStartup + 1 / towerSO.fire_rate;
            Shoot(target);
        }
    }

    private void Shoot(GameObject target)
    {
        GameObject obj = Instantiate(towerSO.projectile_prefab);
        obj.transform.position = transform.position;
        Projectile script = obj.GetComponent<Projectile>();
        script.Initialize(target, 4f, towerSO.damage);
    }

    private GameObject ChooseTarget()
    {
        GameObject target = null;
        var distance = towerSO.range;

        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) <= distance)
            {
                target = enemy;
                distance = Vector2.Distance(enemy.transform.position, transform.position);
            }
        }

        return target;
    }
}
