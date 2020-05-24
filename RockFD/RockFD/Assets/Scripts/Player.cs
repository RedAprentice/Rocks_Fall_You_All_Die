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
    [SerializeField] private int maxhp = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis(xControl);
        yMove = Input.GetAxis(yControl);
        xyMovement();
    }

    private void xyMovement()
    {
        if ( xMove != 0 || yMove != 0 )
        {
            player.GetComponent<Rigidbody>().velocity.Set( xMove * movementFactor, 0, yMove * movementFactor );
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
}
