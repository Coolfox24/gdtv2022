using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField] private float turnSpeed = 90;
    EnemySpawner spawner;
    GameObject target;
    HashSet<GameObject> alreadyDmgd;
    Rigidbody2D rbody;
    public override void Setup(float amount, int hits)
    {
        dmg = amount;
        remainingHits = hits;

        alreadyDmgd = new HashSet<GameObject>();

        spawner = FindObjectOfType<EnemySpawner>();
        rbody = GetComponent<Rigidbody2D>();
        TargetEnemy();
    }

    public override void Setup(float amount)
    {
        dmg = amount;
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            TargetEnemy();
            return;
        }

        //Turn to face target
        Vector2 vectorToTarget = target.transform.position - transform.position;
        vectorToTarget.Normalize();
        float rotateAmount = Vector3.Cross(vectorToTarget, transform.up).z;
        rbody.angularVelocity = -turnSpeed * rotateAmount;
        rbody.velocity = speed * transform.up;
    }

    private void TargetEnemy()
    {   
        if(spawner.curEnemies.Count == 0)
        {
            target = null;
            Destroy(this.gameObject);
            return;
        }

        //Get closest enemy to go towards
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach(GameObject enemy in spawner.curEnemies)
        {
            if(alreadyDmgd.Contains(enemy))
            {
                continue;
            }

            float eDistance = (transform.position - enemy.transform.position).magnitude;

            if(eDistance < distance)
            {   
                distance = eDistance;
                closest = enemy;
            }
        }
        if(closest != null)
        {
            target = closest;
        }
        else
        {
            target = null;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Deal Dmg to other Entity
        if(other.gameObject.tag == "Player")
        {
            return;
        }
        if(other.gameObject.tag == "Enemy" && remainingHits > 0)
        {
            remainingHits--;
            other.GetComponent<Health>().TakeDamage(dmg);
            alreadyDmgd.Add(other.gameObject);
            TargetEnemy();
        }

        if(remainingHits <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        //Add in a pass through stat here as well that we check before destroying
    }
}
