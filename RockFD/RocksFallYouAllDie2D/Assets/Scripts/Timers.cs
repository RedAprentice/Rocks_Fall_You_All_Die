using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timers : MonoBehaviour
{
    //Variables
    [SerializeField]
    private bool isAlive = true;
    public float playerLastShotTimer;
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
    private bool newBestTime;

    #region Singleton
    public static Timers Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0.0f;
        enemyRampedTimer = enemyTimer;
        rockRampedTimer = rockTimer;
        playerLastShotTimer = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustTimers();
        PrintTimers();
    }

    private void AdjustTimers()
    {
        if (isAlive == true)
        {
            totalTime += Time.deltaTime;
        }
        else if(isAlive == false)
        {
            HighScore();
        }
        if (rockTimer > 0.0f)
        {
            rockTimer -= Time.deltaTime;
        }
        else if (rockTimer <= 0.0f)
        {
            //Rocks fall
            rockTimer = 0.0f;
            //Reset the timer
            resetTimer(ref rockTimer, ref rockRampedTimer, rockRampFactor, rockMinTime);
        }

        if (enemyTimer > 0.0f)
        {
            enemyTimer -= Time.deltaTime;
        }
        else if (enemyTimer <= 0.0f)
        {
            //Enemies spawn
            enemyTimer = 0.0f;
            //Reset the timer
            resetTimer(ref enemyTimer, ref enemyRampedTimer, enemyRampFactor, enemyMinTime);
        }

        playerLastShotTimer += Time.deltaTime;
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

    private void PrintTimers()
    {
        string timeString = TimeSpan.FromSeconds((double)totalTime).ToString();
        timeString = timeString.Substring(3, 5);

        totalTimeUI.text = ("Alive For: " + timeString);

        if (newBestTime == true)
        {
            PlayerPrefs.SetString("BestTime", timeString);
            PlayerPrefs.SetFloat("BestTimeFloat", totalTime);
            PlayerPrefs.Save();
        }
    }

    void HighScore()
    {
        if(PlayerPrefs.GetFloat("BestTimeFloat") < totalTime)
        {
            newBestTime = true;
        }
        else
        {
            newBestTime = false;
        }
    }


}
