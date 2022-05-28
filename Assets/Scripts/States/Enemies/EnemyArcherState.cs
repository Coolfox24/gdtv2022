using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherState : EnemyBaseState
{
    float cooldownRemaining;

    public EnemyArcherState(EnemyStateMachine esm) : base(esm)
    {
    }

    public override void OnEnter()
    {
        cooldownRemaining = stateMachine.Stats.specialCooldown;
    }

    public override void OnTick(float deltaTime)
    {
        base.OnTick(deltaTime);

        Debug.Log(cooldownRemaining);
        //Archery Code here
        if((cooldownRemaining -= deltaTime) < 0)
        {
            cooldownRemaining = stateMachine.Stats.specialCooldown;
            Vector2 dir = stateMachine.transform.position - stateMachine.playerPos.position;
            dir.Normalize();
            FireWeapon(dir);
        }
    }

    private void FireWeapon(Vector2 facing)
    {
        float totalProj = 3;
        float angle = Mathf.Atan2(facing.x, -facing.y) * Mathf.Rad2Deg;
        float FiringArc = 45; //Every 10 area increases arc of fire by firing arc

        Quaternion left = Quaternion.Euler (new Vector3(0f,0f,angle - (FiringArc/2)));
        Quaternion right = Quaternion.Euler (new Vector3(0f,0f,angle + (FiringArc/2)));

        for(int i = 0; i < totalProj; i++)
        {
            GameObject proj = GameObject.Instantiate(stateMachine.Stats.specialProjectile, stateMachine.transform.position, Quaternion.Lerp(left, right, i/(totalProj-1)));
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.velocity =  proj.transform.up * (5); 
            Projectile p = proj.GetComponent<Projectile>();
            p?.Setup(stateMachine.Stats.Dmg,
                    1
                    );
        }
    }
}

