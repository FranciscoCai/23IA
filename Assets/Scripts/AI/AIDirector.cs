using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AIDirector : MonoBehaviour
{
    public static AIDirector instance;
    public Transform[] A_alarmTransform;
    public Transform[] A_groupingsPoints;
    public Transform[] A_thiefHidePoints;
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
    public Vector3 WorkerClosePoint(Transform agent, Transform[] points)
    {
        float distance = 0;
        Vector3 destination = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            float actualDistance = Vector3.Distance(points[i].transform.position, agent.transform.position);
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
