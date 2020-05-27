using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IRock
{
    public enum RockType
    {
        Normal,
        Bomb
    }

    public RockType myType;

    public void OnSpawn()
    {
        // Find the information needed to mark self as correct type of rock
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
