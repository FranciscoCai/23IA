using Unity.AI.Navigation;
using UnityEngine;

public class Flee_Thief : StateMachineBehaviour
{
    private GameObject m_Thief;
    private UnityEngine.AI.NavMeshAgent m_Agent;
    private NavMeshLink[] m_LinkTarget;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Thief = animator.gameObject;
        m_Agent = m_Thief.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_LinkTarget = FindObjectsByType<NavMeshLink>(FindObjectsSortMode.None);
        float distance = 0;
        Vector3 destination = Vector3.zero;
        for (int i = 0; i < m_LinkTarget.Length; i++)
        {
            float actualDistance = Vector3.Distance(m_LinkTarget[i].transform.TransformPoint(m_LinkTarget[i].endPoint), m_Thief.transform.position);
            if (distance == 0)
            {
                distance = actualDistance;
                destination = m_LinkTarget[i].transform.TransformPoint(m_LinkTarget[i].endPoint);
            }
            else if (actualDistance <= distance)
            {
                distance = actualDistance;
                destination = m_LinkTarget[i].transform.TransformPoint(m_LinkTarget[i].endPoint);
            }
        }
        m_Agent.SetDestination(destination);
        Debug.Log(destination);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        FindMaintenance();
    }
    private void FindMaintenance()
    {


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
