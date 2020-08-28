using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explodeTime; // moving forward on assumption that each bomb has a local timer
    [SerializeField] private int bombDamage = 25;
    [SerializeField] private float explosionRadius = 2;
    [SerializeField] private LayerMask explosionLayerMask;
    private Collider2D[] objInExplosion;

    // MAKE SURE TO IMPLEMENT THIS LATER
    public void SpawningBehavior()
    {
        explodeTime = Timers.Instance.bombTimer;
    }

    // Update is called once per frame
    void Update()
    {
        explodeTime -= Time.deltaTime;
        if (explodeTime <= 0.0f)
        {
            getObjHit();
            explodeRock();
            explodeDamage();
            // this is where we'd wait for animations to resolve before removing them.
            // or maybe we just have the animation trigger this too.
            deactivateSelf();
        }
    }



    // load the objects that might be affected into objInExplosion
    private void getObjHit()
    {
        objInExplosion = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
    }

    // 'blow up' rocks within objInExplosion
    // NOTE: Identification will use Tags. Make sure to properly TAG everything on spawn!
    private void explodeRock()
    {
        foreach (Collider2D hit in objInExplosion)
        {
            if (hit.tag == "Rock")
            {
                Debug.Log(hit.gameObject);
                hit.GetComponent<Rock>().explodeSelf();
            }
        }
    }

    // damage NPCs and Player
    private void explodeDamage()
    {
        foreach (Collider2D hit in objInExplosion)
        {
            if (hit.tag == "Player")
            {
                Player ourPlayer = hit.gameObject.GetComponent<Player>();
                Utils.hit(ref ourPlayer.hp, bombDamage);
            }
            else if (hit.tag == "Enemy")
            {
                // see above. Implement after David finishes messing around with Enemies
            }
            //else
            //{
            //    Debug.Log("Explode triggered. Not damagable object." + hit);
            //}
        }
    }

    private void deactivateSelf()
    {
        // can't do it instantly. We got an ANIMATION to go through
        gameObject.SetActive(false);
    }

}
