using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Hide_Thief : StateMachineBehaviour
{
    private GameObject m_Thief;
    private NavMeshAgent m_Agent;

    [SerializeField] private float m_WaitTime;
    private bool isCoroutineRunning = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Thief = animator.gameObject;
        m_Agent = m_Thief.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 destination = AIDirector.instance.ClosestHide(m_Thief.transform.position);
        m_Agent.SetDestination(destination);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int MaintenanceMask = 1 << NavMesh.GetAreaFromName("Maintenance");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(m_Thief.transform.position, out hit, 2.0f, MaintenanceMask))
        {
            m_Agent.speed = animator.GetBehaviours<Search_Thief>()[0].m_InitialVelocity / 2;
        }
        else
        {
            m_Agent.speed = animator.GetBehaviours<Search_Thief>()[0].m_InitialVelocity;
        }
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.1f && !isCoroutineRunning)
        {
            isCoroutineRunning = true;
            animator.GetComponent<AIData_Thief>().StartCoroutine(WaitMaintenance(animator, m_WaitTime));
        }
    }
    private IEnumerator WaitMaintenance(Animator animator, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isCoroutineRunning = false;
        animator.SetTrigger("T_Search");

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
