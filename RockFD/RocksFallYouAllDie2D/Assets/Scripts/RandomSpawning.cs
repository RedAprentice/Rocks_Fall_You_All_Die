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
    // Variables
    [SerializeField] private Transform centralPoint;

    // Enemy Variables
    [SerializeField] private Transform enemyNoSpawnZone;
    [SerializeField] private float enemyNoSpawnRadius;

    // Rock Variables
    [SerializeField] private Transform[] rockNoSpawnZone = new Transform[2]; // top left corner, bottom right corner

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // don't know what to do yet, so this is temp
        randomSeed = Time.time;
        randomSeed = randomSeed - Mathf.FloorToInt(randomSeed);
        Random.InitState( (int)(randomSeed * Mathf.Pow(10f, 9f) ) );
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
