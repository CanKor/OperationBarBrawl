using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{

    //components of a point
    private Vector3 PointTransform;
    private GameObject Prefab;
    private GameObject Body;

    //movement (Initialize)
    private PlacePoints PlacePointMNG;


    /// <summary>
    /// Initialize method. BodyOfPoint = the game object associated with the class Point.
    /// MouseClick = where the point is placed
    /// </summary>
    /// <param name="BodyOfPoint"></param>
    /// <param name="MouseClick"></param>
    private void CreateBody(GameObject BodyOfPoint, Vector3 MouseClick)
    {
        PlacePointMNG = GameObject.FindGameObjectWithTag("PlacePointMNG").GetComponent<PlacePoints>();
        Body = GameObject.Instantiate(BodyOfPoint, MouseClick, Quaternion.identity, PlacePointMNG.gameObject.transform);

    }

    //constructor
    public Point(GameObject prefab, Vector3 newTransform) 
    {
        Prefab = prefab;
        PointTransform = newTransform;
        CreateBody(Prefab, PointTransform);
        
    }


    public void SetPointTransform(Vector3 newTransform)
    {
        PointTransform = newTransform;
    }

    public Vector3 GetPointTransform()
    {
        return PointTransform;
    }

    public GameObject GetPointGameObject()
    {
        return Body;
    }
}


