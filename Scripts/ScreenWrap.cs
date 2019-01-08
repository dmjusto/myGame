using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour 
{
  private float leftLimit;
  private float rightLimit;
  private Camera _cam;
  public float offset;

	// Use this for initialization
	void Start () 
  {
      _cam = Camera.main;
      leftLimit = _cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
      rightLimit = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
  {
      if(transform.position.x < leftLimit - offset)
      {
        Debug.Log("off left side of screen");
        transform.position = new Vector3(rightLimit + offset, transform.position.y, transform.position.z);
      }
      if(transform.position.x > rightLimit + offset)
      {
        Debug.Log("off right side of screen");
        transform.position = new Vector3(leftLimit - offset, transform.position.y, transform.position.z);
      }
	}
}
