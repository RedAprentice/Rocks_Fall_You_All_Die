using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlaceholderCharacterSprite");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check that the player entered trigger
        //Check that the player is not currently in I-frames
        //Deal damage to player health
        Debug.Log("Player Entered Walker Trigger");
    }


    void EnemyMovement()
    {
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) >= 1.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 2.0f * Time.deltaTime);
        }
    }
}
