using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    #region Enum Declaration
    // Enum for rocks and enemy spawns
    enum RockType
    {
        rockNeutral,
        rockSmouldering
    }
    enum EnemyType
    {
        enemyWalkEmpty,
        enemyWalkBomb,
        enemyFlyEmpty,
        enemyFlyBomb
    }
    #endregion

    #region Serialized Variables
    [SerializeField] private Transform centralPoint;

    // Enemy Variables
    [SerializeField] private Transform enemyNoSpawnZone;
    [SerializeField] private float enemyNoSpawnRadius;
    [SerializeField] private GameObject enemyPrefab;

    // Rock Variables
    [SerializeField] private Transform[] rockNoSpawnZone = new Transform[2]; // top left corner, bottom right corner
    [SerializeField] private GameObject rockPrefab;

    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    // Seeding Random
    private Random rand;
    private float randomSeed;

    // Determining next spawn type
    // NOTE: Conservative amounts of bombs for moderately large chain reactions.
    private RockType nextRockSpawn = RockType.rockNeutral;
    private EnemyType nextEnemySpawn = EnemyType.enemyWalkEmpty; // NOTE FOR LATER. First enemy is normal. Second enemy is bomb.

    // timer variables
    private float enemyTimer; // internal timer for spawning enemies
    private float enemyRampedTime; // internal time for timer to be reset to
    private float rockTimer; 
    private float rockRampedTime; 

    // Exclusion Zone variables
    private Vector2 topLeftRock;
    private Vector2 botRightRock;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // don't know what to do yet, so this is temp
        randomSeed = Time.time;
        randomSeed = randomSeed - Mathf.FloorToInt(randomSeed);
        Random.InitState( (int)(randomSeed * Mathf.Pow(10f, 9f) ) );

        // initialize rock exclusion zone
        topLeftRock = rockNoSpawnZone[0].position;
        botRightRock = rockNoSpawnZone[1].position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnEnemy()
    {
        // validate good spawn location
        // decide if multiple needs to be spawned, if so how many
        // decide typings for them
        // instantiate them, if multiple spawned, group together somehow idk.
    }

    private void spawnRock()
    {
        // Generate spawn location candidate
        Vector2 rockSpawnCandidate = new Vector2(Random.value, Random.value); // will need to figure out where the bounds of the play

        // validate good spawn location
        if (validRockSpawn(rockSpawnCandidate))
        {
            // decide if multiple needs to be spawned, if so how many

            // decide typings for them

            // instantiate them, if multiple spawned, group together based on grid
            Instantiate(rockPrefab, rockSpawnCandidate, Quaternion.identity); // move this later with Object Pooling
        }

    }

    private void validEnemySpawn()
    {
        // likely new class. Don't want any stuck enemies
    }

    private bool validRockSpawn( Vector2 spawnQuery )
    {
        // simple. just check exclusion zone and if there is already one

        if (spawnQuery.x > topLeftRock.x && spawnQuery.x < botRightRock.x )
        {
            // invalid x
            return false;
        }
        else
        {
            if (spawnQuery.y < topLeftRock.y && spawnQuery.y > botRightRock.y)
            {
                // invalid y
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
