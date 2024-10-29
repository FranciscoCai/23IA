using System.Collections;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Patrol_Guard : StateMachineBehaviour
{
    private GameObject m_Guard;
    public Transform[] m_Points;
    private NavMeshAgent m_Agent;
    public int m_DestPoint = 0;

    public GameObject m_ViewThief;
    public bool m_Wait = false;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Guard = animator.gameObject;
        m_Points = m_Guard.GetComponent<AIData_Guard>().points;
        m_Agent = m_Guard.GetComponent<NavMeshAgent>();
        m_Agent.autoBraking = false;
        m_Agent.destination = m_Points[m_DestPoint].position;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f && m_Wait == false)
        {
            GotoNextPoint(1);
            animator.GetComponent<AIData_Guard>().StartCoroutine(Wait());
        }
        RaycastHit physicsHit;
        if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 10f))
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.red);
            if (physicsHit.collider.gameObject.CompareTag("Thief"))
            {
                m_ViewThief = physicsHit.collider.gameObject;
                animator.SetTrigger("T_Pursue");
            }
        }
        else
        {
            Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.green);
        }
    }
    public void GotoNextPoint(int addDestination)
    {
        if (m_Points.Length == 0)
            return;
        m_DestPoint = (m_DestPoint + addDestination) % m_Points.Length;
        m_Agent.destination = m_Points[m_DestPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
    }
    public IEnumerator Wait()
    {
        m_Wait = true;
        yield return new WaitForSeconds(0.25f);
        m_Wait = false;
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
