using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCasterState : EnemyBaseState
{
    float cooldownRemaining;

    public EnemyCasterState(EnemyStateMachine esm) : base(esm)
    {
    }

    public override void OnEnter()
    {
        cooldownRemaining = stateMachine.Stats.specialCooldown;
    }

    public override void OnTick(float deltaTime)
    {
        base.OnTick(deltaTime);

        //Caster Logic
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
        float angle = Mathf.Atan2(facing.x, -facing.y) * Mathf.Rad2Deg;

        GameObject proj = GameObject.Instantiate(stateMachine.Stats.specialProjectile, stateMachine.transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity =  proj.transform.up * (4); 
        Projectile p = proj.GetComponent<Projectile>();
        p?.Setup(stateMachine.Stats.Dmg,
                1
                );
        p.speed = 4;
    }
}
