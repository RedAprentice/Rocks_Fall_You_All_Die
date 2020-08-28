using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;
    [SerializeField]
    private float eMoveSpeed = 2.0f;

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

        //Possibility of addition if desired to increase enemy movement speed as well as spawn rate as time goes on.
        //if(eMoveSpeed <= 4.0f)
        //{
        //    eMoveSpeed += (Time.deltaTime / 8);
        //}

        if (Vector2.Distance(transform.position, playerPos) >= 1.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, eMoveSpeed * Time.deltaTime);
        }
    }
}
