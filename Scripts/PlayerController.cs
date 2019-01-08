using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Swing))]
public class PlayerController : MonoBehaviour
{

    //movement fields
    public float runSpeed;
    public float walkSpeed;
    public float[] jumpSpeed = new float[2];
    //public float jumpHeight = 10;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask ground;
    private bool grounded;
    private float dir;
    private Rigidbody2D RB;
    private int jumpCount = 1;
    private bool _falling;
    private Swing swingScript;

    // Use this for initialization
    void Start()
    {
        jumpSpeed = new float[] { 13, 17 };
        RB = gameObject.GetComponent<Rigidbody2D>();
        swingScript = GetComponent<Swing>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
        if (dir < 0)
          {
            transform.eulerAngles = new Vector3(0, 180, 0);
          }
              
          else if(dir > 0)
          {
            transform.eulerAngles = new Vector3(0, 0, 0);
          }

        //jumping
        if(Input.GetButtonDown("Jump") && jumpCount>=0)
        {
            RB.velocity = new Vector2(RB.velocity.x, 0);//stops forces from combining on double jump
            RB.AddForce(Vector2.up * jumpSpeed[jumpCount], ForceMode2D.Impulse);
            jumpCount--;
        }

        //resets the ability to jump only after you land
        if(_falling  && grounded)
        {
            _falling = false;
            jumpCount = 1;
        }

        PrintKeys();
    }

    private void FixedUpdate()
    {
        //checks if falling
        if (RB.velocity.y < 0)
        {
            _falling = true;
        }
        //check for grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
        if(!swingScript.swinging)
        {
          RB.velocity = new Vector2(dir * runSpeed, RB.velocity.y);
        }
        
    }

    private void PrintKeys()
    {
        foreach (KeyCode myButton in Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(myButton))
            {
                Debug.Log(myButton);
            }
        }
    }
}
