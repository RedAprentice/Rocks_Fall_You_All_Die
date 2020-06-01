using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enum Declaration
// Enum for rocks and enemy spawns
public enum RockType
{
    rockNeutral,
    rockSmouldering
}
public enum EnemyType
{
    enemyWalkEmpty,
    enemyWalkBomb,
    enemyFlyEmpty,
    enemyFlyBomb
}
public enum Validity
{
    Undetermined, Valid, Invalid, Rock
}
#endregion

public class RandomSpawning : MonoBehaviour
{

    #region Serialized Variables
    [SerializeField] private Transform centralPoint;

    // Enemy Variables
    [SerializeField] private Transform enemyNoSpawnZone;
    [SerializeField] private float enemyNoSpawnRadius;

    // Rock Variables
    [SerializeField] private Transform[] rockNoSpawnZone = new Transform[2]; // top left corner, bottom right corner
    #endregion

    #region Public Variables
    public Validity[,] validSpawns;
    public int mapSizeX;
    public int mapSizeY;
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

        // initialize the spawns array
        validSpawns = new Validity[mapSizeX,mapSizeY];
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

        // DEBUG
        rockSpawnCandidate = new Vector2(2.5f, 2.5f);

        // validate good spawn location
        if (checkRockSpawn(rockSpawnCandidate))
        {
            // decide if multiple needs to be spawned, if so how many

            // decide typings for them
            nextRockSpawn = RockType.rockSmouldering;

            // instantiate them, if multiple spawned, group together based on grid
            ObjectPooler.Instance.spawnRock(rockSpawnCandidate, nextRockSpawn);
        }

    }

    private void validEnemySpawn()
    {
        // likely new class. Don't want any stuck enemies
    }

    private bool checkRockSpawn( Vector2 spawnQuery )
    {
        // simple. just check exclusion zone and if there is already one

        if (spawnQuery.x >= topLeftRock.x && spawnQuery.x <= botRightRock.x )
        {
            // invalid x
            return false;
        }
        else
        {
            if (spawnQuery.y <= topLeftRock.y && spawnQuery.y >= botRightRock.y)
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

    // Checks a single tile to determine if something can be spawned on the tile
    private bool checkValidity( Vector2 tile )
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (validSpawns[x,y] == Validity.Valid) return true;
        else return false;
    }

    // Creates a new mapping for the validity map
    private void generateSpawnMapping()
    {
        cleanSpawnMapping();
        int x = (int)topLeftRock.x;
        int y = (int)topLeftRock.y;
        determineValidity(x, y);
        finalizeValidityMap();
    }

    // determine locally if the surrounding tiles are valid
    // recursively calls self
    // known possible issue: Call stack might get filled.
    private void determineValidity(int x, int y)
    {
        if ( (x + 1) <= mapSizeX)
        {
            if (validSpawns[x + 1, y] == Validity.Undetermined)
            {
                validSpawns[x + 1, y] = Validity.Valid;
                determineValidity(x + 1, y);
            } 
        }
        if ( (x - 1) <= 0)
        {
            if (validSpawns[x - 1,y] == Validity.Undetermined)
            {
                validSpawns[x - 1, y] = Validity.Valid;
                determineValidity(x - 1, y);
            }
        }
        if ( (y + 1) <= mapSizeY)
        {
            if (validSpawns[x, y + 1] == Validity.Undetermined)
            {
                validSpawns[x, y + 1] = Validity.Valid;
                determineValidity(x, y + 1);
            }
        }
        if ( (y - 1) <= 0)
        {
            if (validSpawns[x, y - 1] == Validity.Undetermined)
            {
                validSpawns[x, y - 1] = Validity.Valid;
                determineValidity(x, y - 1);
            }
        }
    }

    // Turns any space that is not a Rock into Undetermined
    private void cleanSpawnMapping()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                if (!(validSpawns[i, j] == Validity.Rock))
                {
                    validSpawns[i, j] = Validity.Undetermined;
                }
            }
        }
    }

    // Meant to only be called at the end of generate valid mapping
    // Turns any leftover Undetermined tiles into Invalid tiles
    private void finalizeValidityMap()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                if ((validSpawns[i, j] == Validity.Undetermined))
                {
                    validSpawns[i, j] = Validity.Invalid;
                }
            }
        }
    }

}
