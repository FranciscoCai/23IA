using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pursue_Guard : StateMachineBehaviour
{
    private GameObject m_Guard;
    private NavMeshAgent m_Agent;
    public int m_DestPoint = 0;

    public Vector3 m_LastPosition;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Guard = animator.gameObject;
        m_Agent = m_Guard.GetComponent<NavMeshAgent>();
        m_Agent.SetDestination(animator.GetBehaviours<Patrol_Guard>()[0].m_ViewThief.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit physicsHit;
        if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 10f))
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.red);
            if (physicsHit.collider.gameObject.CompareTag("Thief"))
            {
                m_LastPosition = physicsHit.collider.gameObject.transform.position;
                m_Agent.SetDestination(physicsHit.collider.gameObject.transform.position);
            }
            else
            {
                animator.SetTrigger("T_Seek");
            }
        }
        else
        {
            animator.SetTrigger("T_Seek");
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.green);
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
