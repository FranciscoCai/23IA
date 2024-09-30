using UnityEngine;
using UnityEngine.AI;
public class AIMovement : MonoBehaviour
{
    [SerializeField] private Transform Goal;
    private NavMeshAgent m_NavAgentComponent;
    void Start()
    {
        m_NavAgentComponent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Goal != null) 
        { 
        m_NavAgentComponent.destination = Goal.position;
            if ()
            {
                Debug.Log(1111111);
            }
        }
    }
}
