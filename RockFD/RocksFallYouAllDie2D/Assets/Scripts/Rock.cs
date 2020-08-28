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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explodeSelf()
    {
        if (myType == RockType.Normal)
        {
            // play ANIMATION that ends with self death
        }
        else if (myType == RockType.Bomb)
        {
            // trigger a bomb explosion on self after ANIMATION
            // kill self
        }
        else
        {
            Debug.Log("Error in Rock.cs: myType (RockType) is not set properly");
        }
    }
}
