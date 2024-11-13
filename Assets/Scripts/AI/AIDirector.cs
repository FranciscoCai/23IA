using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AIDirector : MonoBehaviour
{

    public static AIDirector instance;
    private GameObject[] A_alarmTransform;
    private GameObject[] A_groupingsPoints;
    private GameObject[] A_thiefHidePoints;
    private GameObject[] A_thiefSearchPoints;
    private GameObject[] A_workerPosingPoints;

    private GameObject[] A_workers;
    private GameObject[] A_thiefs;
    private GameObject[] A_guards;
    private Transform A_thiefFinalPosition;
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
        A_workers = GameObject.FindGameObjectsWithTag("Worker");
        A_thiefs = GameObject.FindGameObjectsWithTag("Thief");
        A_guards = GameObject.FindGameObjectsWithTag("Guard");
    }
    public void ThiefFinalPosition(Transform position)
    {
        A_thiefFinalPosition = position;
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
        foreach (GameObject workers in A_workers)
        {
            workers.GetComponent<Animator>().SetTrigger("T_Alarm");
        }
        foreach (GameObject thiefs in A_thiefs)
        {
            thiefs.GetComponent<Animator>().SetTrigger("T_Alarm");
            thiefs.GetComponent<Animator>().SetBool("T_OnAlarm", true);
        }
        foreach (GameObject guards in A_guards)
        {
            guards.GetComponent<Animator>().SetTrigger("T_Alarm");
        }
        StartCoroutine(CdAlarm());
    }
    private IEnumerator CdAlarm()
    {
        yield return new WaitForSeconds(20f);
        foreach (GameObject thiefs in A_thiefs)
        {
            thiefs.GetComponent<Animator>().SetTrigger("T_Search");
            thiefs.GetComponent<Animator>().SetBool("T_OnAlarm", false);
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
