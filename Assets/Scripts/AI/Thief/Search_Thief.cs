using UnityEngine;
using UnityEngine.AI;

public class Search_Thief : StateMachineBehaviour
{
    private GameObject m_Thief;
    private Transform[] m_Points;
    private UnityEngine.AI.NavMeshAgent m_Agent;
    private int m_DestPoint = 0;
    private float m_InitialVelocity;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Thief = animator.gameObject;
        m_Points = m_Thief.GetComponent<AIData_Thief>().points;
        m_Agent = m_Thief.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_InitialVelocity = m_Agent.speed;
        m_Agent.autoBraking = false;

        GotoNextPoint();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        int MaintenanceMask = 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Maintenance");
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(m_Thief.transform.position, out hit, 2.0f, MaintenanceMask))
        {
            m_Agent.speed = m_InitialVelocity / 2;
        }
        else
        {
            m_Agent.speed = m_InitialVelocity;
        }
        //Vector3 RayPosition = new Vector3 (_thief.transform.position.x,_thief.transform.position.y +0.5f, _thief.transform.position.x);
        RaycastHit physicsHit;
        if (Physics.Raycast(m_Thief.transform.position + Vector3.up, m_Thief.transform.forward, out physicsHit, 10f))
        {
            Debug.DrawRay(m_Thief.transform.position + Vector3.up, m_Thief.transform.TransformDirection(Vector3.forward) * 10, Color.red);
            if(physicsHit.collider.gameObject.CompareTag("Worker"))
            {
                animator.SetTrigger("T_Hide");
            }
        }
        else
        {
            Debug.DrawRay(m_Thief.transform.position + Vector3.up, m_Thief.transform.TransformDirection(Vector3.forward) * 10, Color.green);
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (m_Points.Length == 0)
            return;
        // Set the agent to go to the currently selected destination.
        m_Agent.destination = m_Points[m_DestPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        m_DestPoint = (m_DestPoint + 1) % m_Points.Length;
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    //OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
