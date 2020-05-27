﻿// https://www.youtube.com/watch?v=tdSmKaJvCoA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolDict;
    public List<Pool> pools;

    public Queue<GameObject> rockQ;
    public Queue<GameObject> enemyQ;

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Create the object pools
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objectPool);
        }

        if (!poolDict.ContainsKey("rock")) Debug.Log("No rock queue detected.");
        else rockQ = poolDict["rock"];

        if (!poolDict.ContainsKey("enemy")) Debug.Log("No enemy queue detected.");
        else enemyQ = poolDict["enemy"];


        //DEBUG

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool spawnRock(Vector2 location, RockType type, string tag = "rock")
    {
        // This will change later and interact with 'type' param

        GameObject rockToSpawn = poolDict[tag].Dequeue();
        rockToSpawn.SetActive(true);
        rockToSpawn.transform.position = location;
        rockToSpawn.transform.rotation = Quaternion.identity;

        poolDict[tag].Enqueue(rockToSpawn);
        return false;
    }

    public bool spawnEnemy()
    {
        return false;
    }
}