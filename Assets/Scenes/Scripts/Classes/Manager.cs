using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<Monster> MonsterList;
    public GameObject MonsterPrefab;
    //private PlacePoints PlacePointMNG;
    private int MonsterCount; //MyIndex in the monster class.
    private int MonstersDied; //Used to locate monsters in the list to be destroyed
    public GameObject EndPoint;
    public GameObject StartingPoint;
    private float threshold;

    //the path (reference the path in PlacePoints.cs)
    private List<Point> ThePath;
    private PlacePoints PlacePointMNG;


    //UI Objects
    public Text Cooldown;
    private float ParsedCooldown;
    private float CountDown;
    private bool MonstersSpawning;

    //Monsterstats
    public Text Speed;
    private float ParsedSpeed;

    void Awake()
    {

        PlacePointMNG = GameObject.FindGameObjectWithTag("PlacePointMNG").GetComponent<PlacePoints>();
        ThePath = PlacePointMNG.TravelPoints; 

        MonsterCount = 0;
        MonstersDied = 0;
        threshold = 0.1f;
        
        MonsterList = new List<Monster>();
        MonstersSpawning = false;

        //slow down time
        //Time.timeScale = 0.1f;
    }

    //use this later...
    //manage various spawn points, etc...
    void InstantiateMonster()
    {
        //after cd ... instantiate
    }

    void StageUpdate()
    {
        CountDown += Time.deltaTime;

        //Tool items taken from UI elements
        ParseUIElements();


        if (CountDown >= ParsedCooldown && ParsedCooldown > 0.0f && MonstersSpawning == true)
        {
            //temporary
            Monster Doot = new Monster(ParsedSpeed, 1, 1, MonsterCount, MonsterPrefab);
            MonsterCount++;
            MonsterList.Add(Doot);
            CountDown = 0.0f;
        }

        for (int i = MonsterList.Count; i > 0; i--)
        {
            //check for object moving too fast to reach end point (not likely to be used in real game, only for testing tool.)
            if (MonsterList[i - 1].CheckDistance(ThePath[MonsterList[i - 1].nowDestination].GetPointTransform(), ThePath[(MonsterList[i - 1].nowDestination) + 1].GetPointTransform()))
            {
                Debug.Log(ThePath[(MonsterList[i - 1].nowDestination) + 1].GetPointGameObject());
                //check for reaching the end point.
                if (ThePath[(MonsterList[i - 1].nowDestination) + 1].GetPointGameObject().name == "EndPoint(Clone)")
                {
                    MonsterList[i - 1].ReachTheEnd();
                    MonsterList.RemoveAt(i - 1);
                    MonstersDied++;
                }
                //normal situation in which the object moves too fast to be within the threshold.
                else
                {
                    float ExtraDistance = Vector3.Magnitude(MonsterList[i - 1].GetBody().transform.position - ThePath[(MonsterList[i - 1].nowDestination) + 1].GetPointTransform());
                    Debug.Log(ExtraDistance);
                    MonsterList[i - 1].UpdateDestination();
                    Vector3 TempDirectionVector3 = ThePath[MonsterList[i - 1].nowDestination + 1].GetPointTransform() - ThePath[(MonsterList[i - 1].nowDestination)].GetPointTransform();
                    MonsterList[i - 1].GetBody().transform.position = ThePath[(MonsterList[i - 1].nowDestination)].GetPointTransform() + TempDirectionVector3.normalized * ExtraDistance;
                    MonsterList[i - 1].GetBody().transform.rotation = Quaternion.LookRotation(TempDirectionVector3);
                }

            }
            //tell the monster where it is moving.
            else if (MonsterList[i-1].Movement(ThePath[MonsterList[i - 1].nowDestination].GetPointTransform(), ThePath[(MonsterList[i - 1].nowDestination) + 1].GetPointTransform()))
            {
                MonsterList[i - 1].UpdateDestination();

                //check for reaching the end point.
                if (Vector3.Distance(MonsterList[i-1].GetBody().transform.position, EndPoint.transform.position) <= threshold) // end of list
                {
                    
                    MonsterList[i-1].ReachTheEnd();
                    MonsterList.RemoveAt(i-1);
                    MonstersDied++;
                    //add more later - player loses points etc.
                } 
            }
        }
    }

    void FixedUpdate()
    {
        //if (LevelPlaying)
        //{
        //    StageUpdate();
        //}
        StageUpdate();
    }

    //UI Relevant
    public void StartMonsterSpawn()
    {
        MonstersSpawning = true;
    }
    public void EndMonsterSpawn()
    {
        MonstersSpawning = false;
    }

    private void ParseUIElements()
    {
        
        float.TryParse(Cooldown.text, out ParsedCooldown);
        float.TryParse(Speed.text, out ParsedSpeed);

        //set default settings if user has not specified anything.
        //values arbitrary.
        if (Cooldown.text == "")
        {
            ParsedCooldown = 1.0f;
        }
        if (Speed.text == "")
        {
            ParsedSpeed = 4.0f;
        }
    }
}