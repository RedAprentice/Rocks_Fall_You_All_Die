using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctionality : MonoBehaviour
{
    private GameObject player;
    private bool playerPaused;
    private Queue<GameObject> projectileDict;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerPaused = player.GetComponent<Player>().isPaused;
        projectileDict = ObjectPooler.Instance.poolDict["projectile"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PauseFunction()
    {
        if (playerPaused != true)
        {
            foreach (GameObject projectile in projectileDict)
            {
                projectile.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        else if (playerPaused == true)
        {
            foreach(GameObject projectile in projectileDict)
            {
                projectile.GetComponent<Rigidbody2D>().simulated = false;
            }
            //Turning off the physics simulation without a for each loop.
            //GameObject currProjectile = projectileDict.Dequeue();
            //currProjectile.GetComponent<Rigidbody2D>().simulated = false;
            //projectileDict.Enqueue(currProjectile);
        }
    }



}
