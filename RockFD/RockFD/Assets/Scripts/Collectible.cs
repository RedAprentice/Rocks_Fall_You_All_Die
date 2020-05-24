using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //Variable
    public int bombCharge;
    public GameObject player;
    [SerializeField]
    private int healthPickup;

    // Start is called before the first frame update
    void Start()
    {
        bombCharge = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter()
    {
        Collection();
    }

    void Collection()
    {
        switch (gameObject.tag)
        {
            case "Bomb":
                bombCharge += 1;
                break;
            case "Health":
                HealPlayer();
                break;
        }
    }

    void HealPlayer()
    {
        player.playerHealth += healthPickup;
    }

}
