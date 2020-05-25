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
        enemyEmpty,
        enemyBomb
    }

    enum TimerType
    {
        rockTime,
        enemyTime
    }
    #endregion

    #region Serialized Variables
    // Variables
    [SerializeField] private Transform centralPoint;

    // Enemy Variables
    [SerializeField] private Transform enemyNoSpawnZone;
    [SerializeField] private float enemyNoSpawnRadius;
    [SerializeField] private float enemySpawnSpeed; // Base time for spawning interval
    [SerializeField] [Range(0,1000)] private int enemySpawnRamp; // Factor to increase spawning rates
    [SerializeField] private float enemySpawnMinTime; // Smallest interval for things to spawn

    // Rock Variables
    [SerializeField] private Transform[] rockNoSpawnZone = new Transform[2]; // top left corner, bottom right corner
    [SerializeField] private float rockSpawnSpeed; // Base time for spawning interval
    [SerializeField] [Range(0, 1000)] private int rockSpawnRamp; // Factor to increase spawning rates
    [SerializeField] private float rockSpawnMinTime; // Smallest interval for things to spawn
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
    private EnemyType nextEnemySpawn = EnemyType.enemyBomb; // NOTE FOR LATER. First enemy is normal. Second enemy is bomb.

    // timer variables
    private float enemyTimer; // internal timer for spawning enemies
    private float enemyRampedTime; // internal time for timer to be reset to
    private float rockTimer; 
    private float rockRampedTime; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // don't know what to do yet, so this is temp
        randomSeed = Time.time;
        randomSeed = randomSeed - Mathf.FloorToInt(randomSeed);
        Random.InitState( (int)(randomSeed * Mathf.Pow(10f, 9f) ) );

        enemyTimer = enemySpawnSpeed;
        enemyRampedTime = enemySpawnSpeed;

        rockTimer = rockSpawnSpeed;
        rockRampedTime = rockSpawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer -= Time.deltaTime;
        rockTimer -= Time.deltaTime;

        if (enemyTimer <= 0)
        {
            resetTimer(ref enemyTimer, ref enemyRampedTime, enemySpawnRamp, enemySpawnMinTime);
            spawnEnemy();
        }
        if (rockTimer <= 0)
        {
            resetTimer(ref rockTimer, ref rockRampedTime, rockSpawnRamp, rockSpawnMinTime);
            spawnRock();
        }
    }

    // Will reset timer based on spawnRamp
    // Params 
    // timer: internal timer
    // rampTime: internal tracking of time to reset to
    // ramp: ramping factor to use
    // minTime: minimum time for spawning
    private void resetTimer(ref float timer, ref float rampTime, float ramp, float minTime)
    {
        if (timer <= minTime) timer = minTime;
        else
        {
            rampTime = rampTime * (1000 / 1000 + ramp);
            timer = rampTime;
        }
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
        // validate good spawn location
        // decide if multiple needs to be spawned, if so how many
        // decide typings for them
        // instantiate them, if multiple spawned, group together based on grid
    }

    private void validEnemySpawn()
    {
        // likely new class. Don't want any stuck enemies
    }

    private void validRockSpawn()
    {
        // simple. just check exclusion zone and if there is already one
    }
}
