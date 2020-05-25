using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject player;
    [SerializeField] private string xControl = "Horizontal";
    [SerializeField] private string yControl = "Vertical";
    [SerializeField] private float movementFactor = 1.0f;
    private float xMove = 0.0f;
    private float yMove = 0.0f;
    private Vector3 xMoveV;
    private Vector3 yMoveV;
    public int hp;
    public int maxhp = 100;
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
            Vector3 playerMovement = new Vector3(xMove * movementFactor, 0, yMove * movementFactor);
            player.GetComponent<Rigidbody>().velocity = playerMovement;
            Debug.Log(player.GetComponent<Rigidbody>().velocity);
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
}
