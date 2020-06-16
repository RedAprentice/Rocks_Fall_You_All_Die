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
    [SerializeField] private int rockSpawnRingFactor = 20;
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

    // Misc. Rock Variables
    private int maxRockCount;
    private int rockCount = 0;
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

        // initialize maxRockCount
        maxRockCount = 200; // hard coded for now. Should be the max count in the objectPooler
        initMaxRockCount();

        // initialize the spawns array
        validSpawns = new Validity[mapSizeX,mapSizeY];
        generateSpawnMapping();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timers.Instance.rockTimer <= 0.0f && (rockCount < maxRockCount))
        {
            spawnRock();
        }

        //debugDump();
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
        Vector2 rockSpawnCandidate = Vector2.zero;
        bool finish = false;
        int count = 0;
        while (!finish)
        {
            finish = randomPointOnRing(count, ref rockSpawnCandidate);
            count++;
            if ((count > mapSizeX / 2) || (count > mapSizeY / 2))
            {
                Debug.Log("Count too big boi");
                count = 0;
            }
        }

        // validate good spawn location
        if (finish)
        {
            // decide if multiple needs to be spawned, if so how many

            // decide typings for them

            // instantiate them, if multiple spawned, group together based on grid
            ObjectPooler.Instance.spawnRock(rockSpawnCandidate, nextRockSpawn);
            rockCount++;
            Debug.Log("Spawned a Rock");

            // mark space as rock and generate new valid mapping
            validSpawns[(int)rockSpawnCandidate.x, (int)rockSpawnCandidate.y] = Validity.Rock;
            generateSpawnMapping();
        }
        else
        {
            Debug.Log("We couldn't get a valid spawn location.");
        }

    }

    private void validEnemySpawn()
    {
        // likely new class. Don't want any stuck enemies
    }

    // verifies that query is not in the exclusion zone
    private bool checkRockSpawn( Vector2 spawnQuery )
    {
        if (spawnQuery.x < topLeftRock.x || spawnQuery.x > botRightRock.x)
        {
            return true;
        }
        else
        {
            if (spawnQuery.y > topLeftRock.y || spawnQuery.y < botRightRock.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Checks a single tile to determine if something can be spawned on the tile
    private bool checkRockValidity( Vector2 tile )
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (((validSpawns[x, y] == Validity.Valid) || (validSpawns[x,y] == Validity.Undetermined)) && checkRockSpawn(tile)) return true;
        else return false;
    }

    // Creates a new mapping for the validity map
    private void generateSpawnMapping()
    {
        cleanSpawnMapping();
        int x = (int)topLeftRock.x;
        int y = (int)topLeftRock.y;
        determineValidity(x, y);
        // finalizeValidityMap();
    }

    // determine locally if the surrounding tiles are valid
    // recursively calls self
    // known possible issue: Call stack might get filled.
    private void determineValidity(int x, int y)
    {
        // Debug.Log("X: " + x + " Y: " + y);
        if ( (x + 1) < mapSizeX)
        {
            if (validSpawns[x + 1, y] == Validity.Undetermined)
            {
                validSpawns[x + 1, y] = Validity.Valid;
                determineValidity(x + 1, y);
            } 
        }
        if ( (x - 1) >= 0)
        {
            if (validSpawns[x - 1, y] == Validity.Undetermined)
            {
                validSpawns[x - 1, y] = Validity.Valid;
                determineValidity(x - 1, y);
            }
        }
        if ( (y + 1) < mapSizeY)
        {
            if (validSpawns[x, y + 1] == Validity.Undetermined)
            {
                validSpawns[x, y + 1] = Validity.Valid;
                determineValidity(x, y + 1);
            }
        }
        if ( (y - 1) >= 0)
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

    // Params: Ring we want to pull from, tile coordinate variable (by ref)
    // Returns: bool if successfully found one in the chosen ring, tile coordinate (by ref)
    private bool randomPointOnRing(int ring, ref Vector2 tile)
    {

        if ((ring > (mapSizeY / 2)) || (ring > (mapSizeX / 2)))
        {
            Debug.Log("Ring Size Invalid. Ring: " + ring);
            return false;
        }

        int randX;
        int randY;

        // Define the ring
        int xLower = ring;
        int xUpper = mapSizeX - xLower - 1;
        int yLower = ring;
        int yUpper = mapSizeY - yLower - 1;

        // Attempt to find a valid point in the ring
        bool valid = false;
        bool inRing;
        int tries = 0;
        while (!valid && tries < rockSpawnRingFactor)
        {
            inRing = false;

            // Generate a new point that is within the ring we are searching
            while (!inRing)
            {
                // generate new point
                randX = Random.Range(0, mapSizeX);
                randY = Random.Range(0, mapSizeY);

                if ((randX == xUpper) || (randX == xLower))
                {
                    if ((randY >= yLower) && (randY <= yUpper))
                    {
                        inRing = true;
                    }
                }
                else if((randY == yUpper) || (randY == yLower)){
                    if ((randX >= xLower) && ( randX <= xUpper))
                    {
                        inRing = true;
                    }
                }
                else
                {
                    inRing = false;
                }

                // when a point in ring is found, make a new tile
                if (inRing)
                {
                    tile = new Vector2(randX, randY);
                }
            }

            valid = checkRockValidity(tile);
            tries++;
        }

        // Debug.Log("Tries attempted: " + tries);

        if (valid) return true;
        else return false;
    }

    // Generates maxRockCount based on the exclusion zone and map size
    private void initMaxRockCount()
    {
        int excludeX = (int)(botRightRock.x) - (int)(topLeftRock.x) + 1;
        int excludeY = (int)(topLeftRock.y) - (int)(botRightRock.y) + 1;
        int excludeArea = excludeX * excludeY;
        int mapArea = mapSizeX * mapSizeY;
        int tempArea = mapArea - excludeArea;
        if (tempArea < maxRockCount)
        {
            maxRockCount = tempArea;
        }
    }

    // debug dump
    private void debugDump()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                Debug.Log("Location (" + i + ", " + j + "): " + validSpawns[i, j]);
            }
        }
    }

}
