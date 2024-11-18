using UnityEngine;
using UnityEngine.AI;

public class Alarm_Guard : StateMachineBehaviour
{
    private GameObject m_Guard;
    private NavMeshAgent m_Agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Guard = animator.gameObject;
        m_Agent = m_Guard.GetComponent<NavMeshAgent>();
        m_Agent.SetDestination(AIDirector.instance.GuardAlarmPosition());
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
        {
            animator.SetTrigger("T_Scan");
        }
        RaycastHit physicsHit;
        if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 10f))
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.red);
            if (physicsHit.collider.gameObject.CompareTag("Thief"))
            {
                animator.SetTrigger("T_Pursue");
            }
            else
            {
            }
        }
        else
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.green);
        }
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
