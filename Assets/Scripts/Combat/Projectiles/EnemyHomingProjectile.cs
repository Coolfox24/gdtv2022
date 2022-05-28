using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingProjectile : Projectile
{
    [SerializeField] private float turnSpeed = 90;
    EnemySpawner spawner;
    GameObject target;

    Rigidbody2D rbody;
    public override void Setup(float amount, int hits)
    {
        dmg = amount;
        remainingHits = hits;



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
        target = spawner.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Deal Dmg to other Entity
        if(other.gameObject.tag == "Enemy")
        {
            return;
        }
        if(other.gameObject.tag == "Player" && remainingHits > 0)
        {
            remainingHits--;
            other.GetComponent<PlayerStateMachine>().TakeDmg(dmg);
            TargetEnemy();
        }

        if(remainingHits <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        //Add in a pass through stat here as well that we check before destroying
    }
}
