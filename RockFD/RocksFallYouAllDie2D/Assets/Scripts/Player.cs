using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject player;

    #region Controls Variables

    [SerializeField] private string xControl = "Horizontal";
    [SerializeField] private string yControl = "Vertical";
    private float xMove = 0.0f;
    private float yMove = 0.0f;
    private Vector2 xMoveV;
    private Vector2 yMoveV;

    #endregion

    #region Player Stats

    public int hp;
    public int maxhp = 100;
    public float movementFactor = 7.0f;
    public float atkSpeed = 1.0f; // Times to shoot per second
    public float atkDamage = 1.0f;
    public float atkVelocity = 15.0f;
    public int dmgProjectile = 9;
    public int dmgContact = 19;

    #endregion

    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu = GameObject.Find("OptionsMenu");
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis(xControl);
        yMove = Input.GetAxis(yControl);
        xyMovement();
        Menu();
    }

    private void xyMovement()
    {
        if ( (xMove != 0) || (yMove != 0) )
        {
            Vector2 playerMovement = new Vector2(xMove * movementFactor, yMove * movementFactor);
            player.GetComponent<Rigidbody2D>().velocity = playerMovement;
            //Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
        }
    }

    // might move this to utils class.
    public void hit(ref int hp, int damage)
    {
        hp -= damage;
        if ( hp < 0 )
        {
            hp = 0;
        }
    }

    public void heal(ref int hp, int healing, int maxhp)
    {
        hp += healing;
        if ( hp > maxhp)
        {
            hp = maxhp;
        }
    }

    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused != true)
            {
                optionsMenu.SetActive(true);
                isPaused = true;
            }
            else if(isPaused == true)
            {
                optionsMenu.SetActive(false);
                isPaused = false;
            }
        }
    }

    public void UnpauseButton()
    {
        isPaused = false;
    }

    public bool fire()
    {
        // grab a projectile from the object pool of player shots

        // make sure to set up the interface for the shots and use it to restart startup behavior

        // send it the direction the player intended. Mouse and arrow keys

        return true;
    }
}
