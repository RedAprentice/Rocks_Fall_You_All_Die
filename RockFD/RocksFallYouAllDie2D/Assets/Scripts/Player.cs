using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject player;

    #region Controls Variables

    [SerializeField] private string xControl = "Horizontal";
    [SerializeField] private string yControl = "Vertical";
    [SerializeField] private string fireHControl = "Fire1";
    [SerializeField] private string fireVControl = "Fire2";
    private float xMove = 0.0f;
    private float yMove = 0.0f;
    private float verticalFire = 0.0f;
    private float horizontalFire = 0.0f;
    private Vector2 xMoveV;
    private Vector2 yMoveV;

    #endregion

    #region Player Stats

    public int hp;
    public int maxhp = 100;
    public float movementFactor = 7.0f;
    public float projFireRate = 1.0f; // Times to shoot per second
    public int projDamage = 1;
    public float projVelocity = 15.0f;
    public float projRange = 3.0f; // Seconds
    public int dmgProjectile = 9;
    public int dmgContact = 19;

    #endregion

    [SerializeField]
    private GameObject optionsMenu;
    public bool isPaused;

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
        horizontalFire = Input.GetAxis(fireHControl);
        verticalFire = Input.GetAxis(fireVControl);
        xyMovement();
        if ((horizontalFire >= 0.00001f || horizontalFire <= -0.00001f) || (verticalFire >+0.00001f || verticalFire <= -0.00001f)) fireProjectile();
        Menu();
    }

    private void xyMovement()
    {
        if(isPaused != true)
        {
            if ((xMove != 0) || (yMove != 0))
            {
                Vector2 playerMovement = new Vector2(xMove * movementFactor, yMove * movementFactor);
                player.GetComponent<Rigidbody2D>().velocity = playerMovement;
                //Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
            }
        }
        else if(isPaused == true)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // might move hit() and heal() to utils class.
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

    public void fireProjectile()
    {
        Vector2 fireDir;

        if ( horizontalFire >= 0.00001f)
        {
            if (verticalFire >= 0.00001f)
            {
                fireDir = new Vector2 (1,1);
            }
            else if (verticalFire <= -0.00001f)
            {
                fireDir = new Vector2(1, -1);
            }
            else
            {
                fireDir = new Vector2(1, 0);
            }
        } else if( horizontalFire <= -0.00001f)
        {
            if (verticalFire >= 0.00001f)
            {
                fireDir = new Vector2(-1, 1);
            }
            else if (verticalFire <= -0.00001f)
            {
                fireDir = new Vector2(-1, -1);
            }
            else
            {
                fireDir = new Vector2(-1, 0);
            }
        }
        else
        {
            if (verticalFire >= 0.00001f)
            {
                fireDir = new Vector2(0, 1);
            }
            else if (verticalFire <= -0.00001f)
            {
                fireDir = new Vector2(0, -1);
            }
            else
            {
                Debug.Log("wtf how did you end up here stop");
                fireDir = Vector2.up;
            }
        }

        fireDir = Vector2.ClampMagnitude(fireDir, 1);

        if (Timers.Instance.playerLastShotTimer >= (1.0f/projFireRate) )
        {
            ObjectPooler.Instance.spawnProjectile(player.transform.position, fireDir, projDamage, projVelocity, projRange); // REPLACE VECTOR2.UP please
            Debug.Log("Attempting to Fire Projectile");
            Timers.Instance.playerLastShotTimer = 0.0f;
        }
    }

}
