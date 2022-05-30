using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStateMachine : BaseStateMachine
{   
    [field: SerializeField] public Animator animator {get; private set;}
    public int BossMovingHash = Animator.StringToHash("IsMoving");
    public int BossAttack1Hash = Animator.StringToHash("Attack1");
    public int BossAttack2Hash = Animator.StringToHash("Attack2");
    public int BossDeathHash = Animator.StringToHash("Death");

    public Slider healthSlider;

    public GameObject Attack1;
    float attack1Dmg = 25;
    public GameObject Attack2;

    public Transform playerPos {get; private set;}
    
    public Rigidbody2D body {get; private set;}
    private int phase = 0;

    void Start()
    {
        PlayerStateMachine psm = FindObjectOfType<PlayerStateMachine>();
        this.playerPos = psm.transform;       
        body = GetComponent<Rigidbody2D>();
        curState = new BossIdleState(this, 5f);

        healthSlider.maxValue = GetComponent<Health>().maxHealth;
        healthSlider.value = GetComponent<Health>().maxHealth;
    }

    public override void OnDie()
    {
        animator.SetTrigger(BossDeathHash);
    }

    private void FixedUpdate()
    {
        curState?.OnTick(Time.deltaTime);
        if(phase == 1)
        {
            //spawn enemies here on a timer
        }
        healthSlider.value = GetComponent<Health>().curHealth;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStateMachine>().TakeDmg(10); // TODO give enemies dmg
        }
    }

    //Called from animator
    public void IdleState(float idleTime)
    {
        SwitchState(new BossIdleState(this, idleTime));
    }

    public void SpawnProjectile()
    {
        Debug.Log("Spawning Proj");
        float angle = Mathf.Atan2(transform.position.x - playerPos.position.x, transform.position.y - playerPos.position.y) * Mathf.Rad2Deg;
        GameObject g = Instantiate(Attack1, transform.position, Quaternion.Euler(0, 0, angle));
        EnemyHomingProjectile ehp = g.GetComponent<EnemyHomingProjectile>();
        ehp.Setup(attack1Dmg, 1);
        ehp.speed = 10;
    }

    public void EndGame()
    {
        FindObjectOfType<PlayerStateMachine>().EndGame(false);
    }
}
