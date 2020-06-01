using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctionality : MonoBehaviour
{
    private GameObject player;
    private bool playerPaused;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerPaused = player.GetComponent<Player>().isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
