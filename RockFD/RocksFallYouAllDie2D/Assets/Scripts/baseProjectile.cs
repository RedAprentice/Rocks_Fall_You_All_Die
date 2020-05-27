using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseProjectile : MonoBehaviour, IProjectile
{

    Rigidbody2D projectile;
    Vector2 direction;
    int damage;
    float velocity;

    public void SpawningBehavior(Vector2 dir, int dmg, float vel)
    {
        this.enabled = true;
        velocity = vel;
        damage = dmg;
        direction = dir;
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
        }
        else if (collision.tag == "enemy") {
            // damage enemy
            // deactivate projectile
        } else
        {
            // do nothing
        }

    }
}
