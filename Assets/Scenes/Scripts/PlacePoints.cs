using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePoints : MonoBehaviour
{

    /// <summary>
    /// Place a temporary ghost point at the mouse cursor.
    /// When left click, place a real point
    /// 
    /// Instantiates pathways.
    /// 
    /// Also used, later, for placing towers.
    /// 
    /// For now: hold D to place points. Hold F to place towers.
    /// 
    /// sits on the placepointsMNG
    /// </summary>


    public GameObject GhostPoint;
    public GameObject GhostTower;
    //public GhostPointBehavior[] IsCollided;
    //public Renderer[] RedOrGreen;
    public GhostPointBehavior IsCollided;
    public Renderer RedOrGreen;

    //prefabs
    public GameObject RealPoint;
    public GameObject RealPathway;

    public float maxRange = Mathf.Infinity;
    public int mask = 1 << 9; //check the 9th layermask ("Ground")
    public float yOffset;

    //controls the road taken by monsters
    //public Dictionary<int, Point> TravelPoints = new Dictionary<int, Point>();
    //public int PointIterator;
    public List<Point> TravelPoints = new List<Point>();
    public List<Pathway> TravelRoads = new List<Pathway>();
    private GameObject StartPoint;
    private GameObject EndPoint;

    void Awake()
    {
        StartPoint = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().StartingPoint;
        EndPoint = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().EndPoint;

        //CreateGhostObject(GhostPoint, 0);
        //CreateGhostObject(GhostTower, 1);
        //GhostPoint.SetActive(false);
        GhostTower.SetActive(false);

        //initialize Start and End point in TravelPoints (default, temporary)
        Point Starting = new Point(StartPoint, StartPoint.transform.position);
        Point Ending = new Point(EndPoint, EndPoint.transform.position);
        TravelPoints.Add(Starting);
        TravelPoints.Add(Ending);

        //initialized the path heading from the start point to the end point.
        Pathway InitPathway = new Pathway(RealPathway);
        TravelRoads.Add(InitPathway);
        InitPathway.CreateOrientationForPathway(StartPoint.transform.position, EndPoint.transform.position);
    }

    void Update ()
    {
        if(Input.GetKey(KeyCode.D)) //use index 0 for ghostPoint
        {
            GhostPoint.SetActive(true);
            //make the tower follow the mouse pointer.... maxRange = the farthest distance you can spawn the turret.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRange, mask))
            {
                GhostPoint.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + yOffset), Mathf.Round(hit.point.z));
                GhostPoint.transform.rotation = Quaternion.identity;
            }

            //place point - left click default
            if (Input.GetMouseButtonDown(0) && IsCollided.collided == false)
            {
                //temporary
                Point AdditionalPoint = new Point(RealPoint, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + yOffset), Mathf.Round(hit.point.z)));
                int Temp = TravelPoints.Count - 1;// make sure that EndPoints is always last in the list.
                TravelPoints.Insert(Temp, AdditionalPoint);


                Pathway AdditionalPathway = new Pathway(RealPathway);
                TravelRoads.Add(AdditionalPathway);
                TravelRoads[TravelRoads.Count - 1].CreateOrientationForPathway(TravelPoints[TravelPoints.Count - 2].GetPointGameObject().transform.position,
                    TravelPoints[TravelPoints.Count - 1].GetPointGameObject().transform.position);
                TravelRoads[TravelRoads.Count - 2].CreateOrientationForPathway(TravelPoints[TravelPoints.Count - 3].GetPointGameObject().transform.position,
                    TravelPoints[TravelPoints.Count - 2].GetPointGameObject().transform.position);
            }
        }
        else if (Input.GetKey(KeyCode.F)) //use index 1 for ghostTower
        {
            GhostTower.SetActive(true);
            //make the tower follow the mouse pointer.... maxRange = the farthest distance you can spawn the turret.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRange, mask))
            {
                GhostTower.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + yOffset), Mathf.Round(hit.point.z));
                GhostTower.transform.rotation = Quaternion.identity;
            }

            //place point - left click default
            if (Input.GetMouseButtonDown(0) && IsCollided.collided == false)
            {
                //temporary
                //Point AdditionalPoint = new Point(RealPoint, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + yOffset[1]), Mathf.Round(hit.point.z)));
                //int Temp = TravelPoints.Count - 1;// make sure that EndPoints is always last in the list.
                //TravelPoints.Insert(Temp, AdditionalPoint);

                //Pathway AdditionalPathway = new Pathway(RealPathway);
                //TravelRoads.Add(AdditionalPathway);
                //TravelRoads[TravelRoads.Count - 1].CreateOrientationForPathway(TravelPoints[TravelPoints.Count - 2].GetPointGameObject().transform.position,
                //    TravelPoints[TravelPoints.Count - 1].GetPointGameObject().transform.position);
                //TravelRoads[TravelRoads.Count - 2].CreateOrientationForPathway(TravelPoints[TravelPoints.Count - 3].GetPointGameObject().transform.position,
                //    TravelPoints[TravelPoints.Count - 2].GetPointGameObject().transform.position);
            }
        }
        else
        {
            
            IsCollided.collided = false;
            RedOrGreen.material = IsCollided.valid;
            GhostPoint.SetActive(false);

            //IsCollided[1].collided = false;
            //RedOrGreen[1].material = IsCollided[1].valid;
            //GhostTower.SetActive(false);
        }

    }

    /// <summary>
    /// Used to initialize ghost objects.
    /// index is used to differentiate between them,
    /// with their components stored in arrays.
    /// </summary>
    /// <param name="index"></param>
    //private void CreateGhostObject(GameObject WhichGhost, int index) //fix this later
    //{
    //    Debug.Log(WhichGhost);
    //    yOffset[index] = WhichGhost.transform.localScale.y / 2;
    //    IsCollided[index] = WhichGhost.GetComponent<GhostPointBehavior>();
    //    RedOrGreen[index] = WhichGhost.GetComponent<Renderer>();
    //}
}
