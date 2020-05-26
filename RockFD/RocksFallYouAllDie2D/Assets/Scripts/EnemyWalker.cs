using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check that the player entered trigger
        //Check that the player is not currently in I-frames
        //Deal damage to player health
        Debug.Log("Player Entered Walker Trigger");
    }
}
