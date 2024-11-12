using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AIDirector : MonoBehaviour
{
    public static AIDirector instance;
    public GameObject[] A_alarmTransform;
    public GameObject[] A_groupingsPoints;
    public GameObject[] A_thiefHidePoints;
    public GameObject[] A_thiefSearchPoints;
    public GameObject[] A_workerPosingPoints;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        A_alarmTransform = GameObject.FindGameObjectsWithTag("AlarmP");
        A_groupingsPoints = GameObject.FindGameObjectsWithTag("GroupP");
        A_thiefHidePoints = GameObject.FindGameObjectsWithTag("ThiefHideP");
        A_thiefSearchPoints = GameObject.FindGameObjectsWithTag("ThiefSearchP");
        A_workerPosingPoints = GameObject.FindGameObjectsWithTag("WorkerPosingP");
    }
    public Transform[] GetSearchPoints()
    {
        Transform[] points = new Transform[6];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = A_thiefSearchPoints[i].GetComponent<Transform>();
        }
        return points;
    }
    public Transform[] GetPosingPoints()
    {
        Transform[] points = new Transform[6];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = A_workerPosingPoints[i].GetComponent<Transform>();
        }
        return points;
    }

    // Update is called once per frame
    public void AlarmEfect()
    {
        GameObject[] worker = GameObject.FindGameObjectsWithTag("Worker");
        foreach (GameObject worker2 in worker)
        {
            worker2.GetComponent<Animator>().SetTrigger("T_Alarm");
        }
    }

    public Vector3 ClosestAlarm(Vector3 position)
    {
        return ClosestPoint(position, A_groupingsPoints);
    }
    public Vector3 ClosestWarning(Vector3 position)
    {
        return ClosestPoint(position, A_groupingsPoints);
    }
    public Vector3 ClosestHide(Vector3 position)
    {
        return ClosestPoint(position, A_thiefHidePoints);
    }

    private Vector3 ClosestPoint(Vector3 position, GameObject[] points)
    {
        float distance = 0;
        Vector3 destination = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            float actualDistance = Vector3.Distance(points[i].transform.position, position);
            if (distance == 0)
            {
                distance = actualDistance;
                destination = points[i].transform.position;
            }
            else if (actualDistance <= distance)
            {
                distance = actualDistance;
                destination = points[i].transform.position;
            }
        }
        return destination;
    }
}
