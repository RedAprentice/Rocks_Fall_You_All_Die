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
    [Range(0,1000)] public int rockRampFactor;
    [SerializeField] private float rockMinTime;
    private float rockRampedTimer;
    public float enemyTimer;
    [Range(0,1000)] public int enemyRampFactor;
    [SerializeField] private float enemyMinTime;
    private float enemyRampedTimer;
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

    private void AdjustTimers()
    {
        if (isAlive == true)
        {
            totalTime += Time.deltaTime;
        }

        if (rockTimer >= 0.0f)
        {
            rockTimer -= Time.deltaTime;
        }
        else if (rockTimer <= 0.0f)
        {
            //Rocks fall

            //Reset the timer
            resetTimer(ref rockTimer, ref rockRampedTimer, rockRampFactor, rockMinTime);
        }

        if (rockTimer >= 0.0f)
        {
            rockTimer -= Time.deltaTime;
        }
        else if (rockTimer <= 0.0f)
        {
            //Enemies spawn

            //Reset the timer
            resetTimer(ref enemyTimer, ref enemyRampedTimer, enemyRampFactor, enemyMinTime);
        }
    }

    // Will reset timer based on spawnRamp
    // Params 
    // timer: internal timer
    // rampTime: internal tracking of time to reset to
    // ramp: ramping factor to use
    // minTime: minimum time for spawning
    private void resetTimer(ref float timer, ref float rampTime, float ramp, float minTime)
    {
        if (timer <= minTime) timer = minTime;
        else
        {
            rampTime = rampTime * (1000 / 1000 + ramp);
            timer = rampTime;
        }
    }
}
