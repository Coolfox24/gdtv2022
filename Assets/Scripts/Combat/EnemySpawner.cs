using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    //TODO have 2 lists of gameObjects that we spawn from
    //Limit how many entities we can have in first list 

    //Class will be attached to player and spawn enemies within a radius of player

    //Want some way of changing these parameters when entering different zones
    public float minRange = 3f;
    public float maxRange = 8f;
    public float timeToSpawn = 4f; //Spawn new enemies every 4s
    public float enemySpawnAmount = 2; //Amount of enemies to spawn each time
    [SerializeField] private Tilemap tMap;
    [SerializeField] private Dictionary<RuleTile, List<Spawn_Rule>> spawnRules;
    [SerializeField] private List<Spawn_Rules> SpawnRules;
    [SerializeField] RuleTile invalidSpawnTile;

    [SerializeField] private bool pause = false;

    [SerializeField] private int maxEnemies = 5;
    //[SerializeField] private List<GameObject> disabledEnemies;
    [field: SerializeField] public List<GameObject> curEnemies { get; private set; }

    [SerializeField] private float timeRemaining;
    [SerializeField] private Transform enemies;

    private PlayerStateMachine PSM;

    private void Start()
    {
        PSM = GetComponent<PlayerStateMachine>();

        timeRemaining = timeToSpawn / 2; //Quicker spawn at start of game

        //Create Dictionary Here
        spawnRules = new Dictionary<RuleTile, List<Spawn_Rule>>();
        foreach (Spawn_Rules sr in SpawnRules)
        {
            spawnRules.Add(sr.tileToSpawn, sr.rules);
        }
    }

    void Update()
    {
        if (pause)
        {
            return;
        }

        Spawn();
    }

    private void Spawn()
    {
        if ((timeRemaining -= Time.deltaTime) > 0)
        {
            return;
        }

        timeRemaining = timeToSpawn;
        for (int i = 0; i < enemySpawnAmount; i++)
        {
            RuleTile t;
            Vector2 spawnPos;
            do
            {
                int positiveX = Random.Range((int)0, (int)2);
                int positiveY = Random.Range((int)0, (int)2);
                Vector2 spawnRadius = new Vector2(
                    Random.Range(minRange, maxRange) * (positiveX == 0 ? -1 : 1),
                    Random.Range(minRange, maxRange) * (positiveY == 0 ? -1 : 1)
                    );

                spawnPos = (Vector2)transform.position + spawnRadius;

                t = (RuleTile)tMap.GetTile(tMap.WorldToCell(spawnPos));
            } while (t == invalidSpawnTile || t == null || Physics2D.OverlapBox(spawnPos, Vector2.one * 0.5f, 0));

            //Determine what enemy to spawn here
            GameObject enemy = GetSpawnedType();
            if (enemy == null)
            {
                return;
            }
            enemy = SpawnEnemy(spawnPos, enemy);
            if (enemy == null)
            {
                return;
            }
            enemies.transform.SetParent(enemies);
            //Apply time scaling here? Could also have scaling be based on prefabs
        }
    }

    private GameObject GetSpawnedType()
    {
        try
        {
            RuleTile t = (RuleTile)tMap.GetTile(tMap.WorldToCell(this.transform.position));
            if (spawnRules.ContainsKey(t))
            {
                return GetSpawnedType(spawnRules[t]);
            }
            else
            {
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Log("Error if we care spawning");
        }

        return null;
    }

    private GameObject GetSpawnedType(List<Spawn_Rule> rules)
    {
        float weighting = 0;
        foreach (Spawn_Rule rule in rules)
        {
            weighting += rule.weighting;
        }

        float spawnNum = Random.Range(0, weighting);

        foreach (Spawn_Rule rule in rules)
        {
            if (spawnNum <= rule.weighting)
            {
                return rule.enemy;
            }
            spawnNum -= rule.weighting;
        }

        return null;
    }

    private GameObject SpawnEnemy(Vector2 position, GameObject enemyPrefab)
    {
        if (curEnemies.Count >= maxEnemies)
        {
            return null;
        }

        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        curEnemies.Add(enemy);
        enemy.SetActive(true);

        return enemy;
    }

    public void RegisterDeath(GameObject deadEnemy, float exp)
    {
        curEnemies.Remove(deadEnemy);
        //PSM.AddEXP(exp) //TODO IMPLEMENT THIS
        //disabledEnemies.Add(deadEnemey);
    }

    public void PauseGame()
    {
        pause = true;
        foreach (GameObject enemy in curEnemies)
        {
            EnemyStateMachine e = enemy.GetComponent<EnemyStateMachine>();
            e.SwitchState(new EnemyPauseState(e));
        }
    }

    public void UnpauseGame()
    {
        pause = false;
        foreach (GameObject enemy in curEnemies)
        {
            EnemyStateMachine e = enemy.GetComponent<EnemyStateMachine>();
            e.SwitchState(new EnemyBaseState(e));
        }
    }



    [System.Serializable]
    public struct Spawn_Rules
    {
        [SerializeField] public RuleTile tileToSpawn;
        [SerializeField] public List<Spawn_Rule> rules;
    }

    [System.Serializable]
    public struct Spawn_Rule
    {
        [SerializeField] public GameObject enemy;
        [SerializeField] public float weighting;
    }
}
