using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{

    private float Speed;
    private float Attack;
    private float Health;
    private int MyIndex; //what is this monster's spawn ID
    private GameObject Prefab;
    private GameObject Body;
    //monster needs to now start point as this is where it is instantiated.
    private Vector3 StartPoint;
    //private Vector3 EndPoint;

    //movement (Initialize)
    private PlacePoints PlacePointMNG;
    public int nowDestination; //position of the point the monster walking towards.
    private float threshold = 0.1f;


    public Monster(float speed, float attack, float health, int myindex, GameObject prefab)
    {
        Speed = speed;
        Attack = attack;
        Health = health;
        Prefab = prefab;
        MyIndex = myindex;
        CreateBody(Prefab);
    }
    /// <summary>
    /// Initialize method. BodyOfMonster = the game object associated with the class Monster.
    /// </summary>
    /// <param name="BodyOfMonster"></param>
    private void CreateBody(GameObject BodyOfMonster)
    {
        StartPoint = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().StartingPoint.transform.position;
        Body = GameObject.Instantiate(BodyOfMonster, StartPoint, Quaternion.identity);
        nowDestination = 0;
    }

    //stats setters

    //stats getters
    public float GetAttack()
    {
        return Attack;
    }
    public float GetHealth()
    {
        return Health;
    }
    public float GetSpeed()
    {
        return Speed;
    }
    public GameObject GetBody()
    {
        return Body;
    }
    public int GetIndex()
    {
        return MyIndex;
    }



    /// <summary>
    /// Called by the manager to set the new destination for the mob (once it arrives at its current target destination.)
    /// </summary>
    public void UpdateDestination()
    {
        nowDestination++; 
    }

    public bool Movement(Vector3 posA, Vector3 posB)//this code can be written in fewer lines.
    {
        Vector3 DirectionVector = posB - GetBody().transform.position;
        GetBody().transform.position += DirectionVector.normalized * Time.deltaTime * Speed;
        GetBody().transform.rotation = Quaternion.LookRotation(DirectionVector);
        if (Vector3.Distance(GetBody().transform.position, posB) <= threshold)
        {  
            return true;
        }
        else
        {
            return false;
        }
       
         
    }

    //death conditions
    public void ReachTheEnd()
    {
        //add other things later
        Object.Destroy(GetBody());
        
    }
    public void Die()
    {

    }

    //correction method
    /// <summary>
    /// posA - where I am coming from
    /// posB - where I am going
    /// </summary>
    /// <param name="posA"></param>
    /// <param name="posB"></param>
    /// <returns></returns>
    public bool CheckDistance(Vector3 posA, Vector3 posB)
    {

        if (Vector3.Magnitude(GetBody().transform.position - posA) > Vector3.Magnitude(posB-posA))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}