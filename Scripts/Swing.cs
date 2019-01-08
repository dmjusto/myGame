using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]


public class Swing : MonoBehaviour 
{
  private Rigidbody2D rb;
  private SpringJoint2D jnt;
  private Vector3 rayEnd;
  [System.NonSerialized]
  public bool swinging = false;

  public float ropeAngle = 45;

	// Use this for initialization
	void Start () 
  {
    rb = GetComponent<Rigidbody2D>();
    jnt = GetComponent<SpringJoint2D>();
	}

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Swing"))
    {
      SwingIt();
    }
    else if (Input.GetButtonDown("Jump") && swinging)
    {
      DropRope();
    }
    
    //draw rope
    if(swinging)
    {
      Debug.DrawLine(transform.position, rayEnd, Color.black);
    }
	}
  
  private void SwingIt()
  {
    Debug.Log("you're swinging");
    int mask = 1 << 8;
    mask = ~mask;
    Vector3 swingVector = transform.TransformDirection(Vector3.right + Vector3.up);
    RaycastHit2D hit2D = Physics2D.Raycast(transform.position, swingVector, Mathf.Infinity, mask);
    rayEnd = hit2D.point;
    swinging = true;
    jnt.connectedAnchor = hit2D.point;
    jnt.enabled = true;
  }
  
  private void DropRope()
  {
    swinging = false;
    jnt.enabled = false;
  }
}
