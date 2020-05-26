using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    //Variables
    [SerializeField]
    private bool isAlive;
    public float rockTimer;
    public float totalTime;
    public Text totalTimeUI;


    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0.0f;
        rockTimer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AdjustTimers()
    {
        if (isAlive == true)
        {
            totalTime += Time.deltaTime;
        }

        //Move this to the script that makes rocks fall
        if (rockTimer >= 0.0f)
        {
            rockTimer -= Time.deltaTime;
        }
        else if (rockTimer <= 0.0f)
        {
            //Rocks fall
            rockTimer = 10.0f;
        }
    }
}
