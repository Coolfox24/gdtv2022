using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : BaseStateMachine
{
    public Transform playerPos {get; private set;}
    public Rigidbody2D body {get; private set;}
    [SerializeField] private float MaxDistanceFromPlayer = 15;
    [field: SerializeField] public Animator animator {get; private set;}
    public EnemyStats Stats;
    public float MaxDistanceFromPlayerSqrd {get; private set;}
    private EnemySpawner enemySpawner;
    private LootGenerator lootGenerator;

    public int EnemyDirectionXHash = Animator.StringToHash("X");
    public int EnemyDirectionYHash = Animator.StringToHash("Y");

    //Scriptable Object here to determine what speed & health

    private void Awake()
    {
        GetComponent<Health>().maxHealth = Stats.MaxHealth;
    }

    private void Start()
    {
        MaxDistanceFromPlayerSqrd = MaxDistanceFromPlayer * MaxDistanceFromPlayer;
        PlayerStateMachine psm = FindObjectOfType<PlayerStateMachine>();
        this.playerPos = psm.transform;
        lootGenerator = psm.GetComponent<LootGenerator>();
        body = GetComponent<Rigidbody2D>();
        this.enemySpawner = FindObjectOfType<EnemySpawner>();

        switch(Stats.AIMode)
        {
            case EnemyStats.AISettings.Archer:
                curState = new EnemyArcherState(this);
                break;
            case EnemyStats.AISettings.Caster:
                curState = new EnemyCasterState(this);
                break;
            case EnemyStats.AISettings.Charger:
                curState = new EnemyChargeState(this);
                break;
            default: 
                curState = new EnemyBaseState(this);
                break;
        }
    }

    private void FixedUpdate()
    {
        curState.OnTick(Time.deltaTime);
    }

    public override void OnDie()
    {
        //Disable Here
        gameObject.SetActive(false);
        enemySpawner.RegisterDeath(gameObject, Stats.ExpGiven);
        lootGenerator.GenerateLoot(Stats.LootTable, transform.position);
        GameObject.Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStateMachine>().TakeDmg(Stats.Dmg); // TODO give enemies dmg
        }
    }
}
