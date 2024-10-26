using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Flee_Thief : StateMachineBehaviour
{
    private GameObject m_Thief;
    private UnityEngine.AI.NavMeshAgent m_Agent;
    private NavMeshLink[] m_LinkTarget;
    private NavMeshObstacle[] m_NavObstacleTarget;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Thief = animator.gameObject;
        m_Agent = m_Thief.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_LinkTarget = FindObjectsByType<NavMeshLink>(FindObjectsSortMode.None);
        m_NavObstacleTarget = FindObjectsByType<NavMeshObstacle>(FindObjectsSortMode.None);
        m_NavObstacleTarget = FindObjectsByType<NavMeshObstacle>(FindObjectsSortMode.None);

        // Mover el agente hacia la posici¨®n de escape
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 directionToGurad = m_Thief.transform.position - animator.GetBehaviours<Search_Thief>()[0].m_ViewGuard.transform.position;
        Vector3 fleePosition = m_Thief.transform.position + directionToGurad.normalized * 10f;
        if (Vector3.Distance(m_Thief.transform.position, animator.GetBehaviours<Search_Thief>()[0].m_ViewGuard.transform.position)<10f)
        {
            m_Agent.SetDestination(fleePosition);
        }
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
        {
            animator.SetTrigger("T_Flee");
        }
        foreach (NavMeshObstacle Obstacle in m_NavObstacleTarget)
        {
            if (Vector3.Distance(Obstacle.gameObject.transform.position, m_Thief.transform.position) < 2f)
            {
                Rigidbody rb = Obstacle.gameObject.GetComponent<Rigidbody>();
                Vector3 directionToTarget = Obstacle.transform.position - m_Thief.transform.position;
                rb.AddForce((directionToTarget.normalized), ForceMode.Impulse);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

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
