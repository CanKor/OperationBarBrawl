using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPointBehavior : MonoBehaviour
{
    /// <summary>
    /// This sits on the ghost point and detects collisions.
    /// This probably doesn't need to exist.
    /// </summary>

    public bool collided;
    public Material valid;
    public Material invalid;

    private void Start()
    {
        collided = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster" || other.gameObject.tag == "Point" || other.gameObject.tag == "Pathway")
        {
            gameObject.GetComponent<Renderer>().material = valid;
            collided = false;
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Monster" || other.gameObject.tag == "Point" || other.gameObject.tag == "Pathway")
        {
            gameObject.GetComponent<Renderer>().material = invalid;
            collided = true;
        }
    }
}
