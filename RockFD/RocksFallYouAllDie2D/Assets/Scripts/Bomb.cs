using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explodeTime; // moving forward on assumption that each bomb has a local timer
    [SerializeField] private int bombDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask explosionLayerMask;
    private Collider[] objInExplosion;

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
        objInExplosion = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);
    }

    // 'blow up' rocks within objInExplosion
    // NOTE: Identification will use Tags. Make sure to properly TAG everything on spawn!
    private void explodeRock()
    {
        foreach (Collider hit in objInExplosion)
        {
            if (hit.tag == "Rock") // will probably change this line. Don't know what tags to use yet. Do we need to care about the different rock types?
            {
                hit.gameObject.GetComponent<Rock>().explodeSelf();
            }
        }
    }

    // damage NPCs and Player
    private void explodeDamage()
    {
        foreach (Collider hit in objInExplosion)
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
