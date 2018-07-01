using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour 
{
    /// <summary>
    /// Keep track of the position/movement of the camera.
    /// </summary>
	private Vector3 pos;
	
	void Start ()
    {
		pos = Camera.main.transform.position;
	}
	
	
	void Update () 
	{
		gameObject.transform.position = pos;
	}
}
