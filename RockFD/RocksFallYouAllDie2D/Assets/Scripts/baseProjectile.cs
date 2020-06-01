using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseProjectile : MonoBehaviour, IProjectile
{

    Rigidbody2D projectile;
    Vector2 position;
    Vector2 direction;
    int damage;
    float range;

    public void SpawningBehavior(Vector2 pos, Vector2 dir, int dmg, float vel, float ran)
    {
        this.enabled = true;
        projectile.position = pos;
        projectile.velocity = dir * vel;
        damage = dmg;
        range = ran;
    }

    // Start is called before the first frame update
    void Start()
    {
        projectile = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // go fly
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rock")
        {
            // deactivate projectile
            this.enabled = false;
        }
        else if (collision.tag == "enemy") {
            // damage enemy
            // collision.GetComponent<>().hp -= damage;

            // deactivate projectile
            this.enabled = false;
        } else
        {
            // do nothing
        }

    }
}
