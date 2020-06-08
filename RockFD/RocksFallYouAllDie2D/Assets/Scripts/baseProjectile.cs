using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseProjectile : MonoBehaviour, IProjectile
{
    Vector3 position;
    Vector3 direction;
    int damage;
    float range;

    private float rangeTracker;

    public void SpawningBehavior(Vector3 pos, Vector3 dir, int dmg, float vel, float ran)
    {
        Debug.Log("Adjusting Projectile on Spawn");
        position = pos;
        transform.position = position;
        GetComponent<Rigidbody2D>().velocity = vel * dir;
        damage = dmg;
        range = ran;

        //GetComponent<Transform>().LookAt(vel * new Vector3(dir.x , dir.y, dir.z)); didn't work
        switch (dir.x)
        {
            case float n when (n < -0.01f):
                switch (dir.y)
                {
                    case float m when (m < -0.01f): // -,-
                        transform.rotation = Quaternion.Euler(0,0,135);
                        break;
                    case float m when (m > 0.01f): // -,+
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                        break;
                    default: // -,0
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                }
                break;
            case float n when (n > 0.01f):
                switch (dir.y)
                {
                    case float m when (m < -0.01f): // +,-
                        transform.rotation = Quaternion.Euler(0, 0, 225);
                        break;
                    case float m when (m > 0.01f): // +,+
                        transform.rotation = Quaternion.Euler(0, 0, 315);
                        break;
                    default: // +,0
                        transform.rotation = Quaternion.Euler(0, 0, 270);
                        break;
                }
                break;
            default:
                switch (dir.y)
                {
                    case float m when (m < -0.01f): // 0,-
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case float m when (m > 0.01f): // 0,+
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    default: // 0,0
                        Debug.Log("You crazy");
                        break;
                }
                break;
        }
    }

    private void Update()
    {
        checkRange();
    }

    void checkRange()
    {
        Vector3 distance = position - gameObject.transform.position;

        if (Vector3.SqrMagnitude(distance) >= Mathf.Pow(range, 2))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER");
        Debug.Log("Collided with an: " + collision.tag);

        switch (collision.tag)
        {
            case "Rock":
                gameObject.SetActive(false);
                break;
            case "Enemy":
                gameObject.SetActive(false);
                // do damage
                break;
            case "Wall":
                gameObject.SetActive(false);
                break;
            default:
                // nothing
                break;
        }

    }
}
