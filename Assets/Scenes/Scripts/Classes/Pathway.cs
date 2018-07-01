using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway
{
    /// <summary>
    /// Class for creating roadways.
    /// Use these for interactions with the player as well as preventing turrets from being placed in some places.
    /// </summary>


    private GameObject Prefab;
    private GameObject Body;

    public Pathway(GameObject prefab)
    {
        Prefab = prefab;
        CreateBody(Prefab);
    }

    /// <summary>
    /// Initialize method. BodyOfPoint = the game object associated with the class Pathway.
    /// </summary>
    /// <param name="BodyOfPathway"></param>

    private void CreateBody(GameObject BodyOfPathway)
    {
        Body = GameObject.Instantiate(BodyOfPathway);

    }

    public GameObject GetPathwayGameObject()
    {
        return Body;
    }

    /// <summary>
    /// This method takes in the starting and ending coordinates for the two points that the pathway connects,
    /// and orients the pathway to connect those points.
    /// </summary>
    /// <param name="StartCoordinates"></param>
    /// <param name="EndCoordinates"></param>
    public void CreateOrientationForPathway(Vector3 StartCoordinates, Vector3 EndCoordinates)
    {
        Body.transform.position = (EndCoordinates + StartCoordinates)/ 2; //place at midway point.
        Body.transform.localScale = new Vector3(Vector3.Distance(StartCoordinates, EndCoordinates), Body.transform.localScale.y, Body.transform.localScale.z);


        //need -90.0 degrees because we are rotating around the y axis.
        //I have yet to figure out why this makes sense via trigonometry.
        Vector3 directionVec = EndCoordinates - StartCoordinates;
        float angleTan = Mathf.Atan2(directionVec.x, directionVec.z) * Mathf.Rad2Deg;
        Body.transform.rotation = Quaternion.AngleAxis(angleTan - 90.0f, Vector3.up);
    }
}
