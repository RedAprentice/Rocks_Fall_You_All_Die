using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;
    [SerializeField]
    private float eMoveSpeed = 2.0f;
    private int damage = 1;
    [SerializeField]
    private int eHealth = 10;
    private Player pCS;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlaceholderCharacterSprite");
        pCS = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check that the player entered trigger
        if(other.tag == "Player")
        {
            Utils.hit(ref pCS.hp, damage);
            pCS.HealthCheck();
        }
        if(other.tag == "Projectile")
        {
            
            Utils.hit(ref eHealth, pCS.projDamage);
            HealthCheck();
            
        }
        //Check that the player is not currently in I-frames
        //Deal damage to player health

    }


    void EnemyMovement()
    {
        playerPos = player.transform.position;

        //Possibility of addition if desired to increase enemy movement speed as well as spawn rate as time goes on.
        //if(eMoveSpeed <= 4.0f)
        //{
        //    eMoveSpeed += (Time.deltaTime / 8);
        //}

        if (Vector2.Distance(transform.position, playerPos) >= 0.7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, eMoveSpeed * Time.deltaTime);
        }
    }

    void HealthCheck()
    {
        if(eHealth == 0)
        {
            Debug.Log("Enemy Died");
            gameObject.SetActive(false);
        }
    }
}
