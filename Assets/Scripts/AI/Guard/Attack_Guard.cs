using UnityEngine;
using UnityEngine.AI;

public class Attack_Guard : StateMachineBehaviour
{
    private GameObject m_Guard;
    private NavMeshAgent m_Agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Guard = animator.gameObject;
        m_Agent = m_Guard.GetComponent<NavMeshAgent>();
        m_Agent.ResetPath();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit physicsHit;
        if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 1f))
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 1, Color.red);
            if (physicsHit.collider.gameObject.CompareTag("Thief"))
            {
                physicsHit.collider.gameObject.GetComponent<Animator>().SetTrigger("T_KO");
            }
            else
            {
                animator.SetTrigger("T_Seek");
            }
        }
        else if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 10f))
        {
            if (physicsHit.collider.gameObject.CompareTag("Thief"))
            {
                if (Vector3.Distance(m_Guard.transform.position,physicsHit.collider.gameObject.transform.position)>2f)
                {
                    animator.SetTrigger("T_Pursue");
                }
                else
                {
                    animator.SetTrigger("T_Seek");
                }
            }
            else
            {
                animator.SetTrigger("T_Seek");
            }
        }
        else
        {
            animator.SetTrigger("T_Seek");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
