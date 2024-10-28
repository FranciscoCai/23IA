using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Search_Thief : StateMachineBehaviour
{
    private GameObject m_Thief;
    public Transform[] m_Points;
    private NavMeshAgent m_Agent;
    public int m_DestPoint = 0;
    [SerializeField] private float m_InitialVelocity;

    public GameObject m_ViewGuard;
    public bool m_Wait = false;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Thief = animator.gameObject;
        m_Points = m_Thief.GetComponent<AIData_Thief>().points;
        m_Agent = m_Thief.GetComponent<NavMeshAgent>();
        m_Agent.autoBraking = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f && m_Wait== false)
        {
            GotoNextPoint(1);
            animator.GetComponent<AIData_Thief>().StartCoroutine(Wait());
        }
        int MaintenanceMask = 1 << NavMesh.GetAreaFromName("Maintenance");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(m_Thief.transform.position, out hit, 2.0f, MaintenanceMask))
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
            if (physicsHit.collider.gameObject.CompareTag("Worker"))
            {
                animator.SetTrigger("T_Hide");
            }
            else if (physicsHit.collider.gameObject.CompareTag("Guard"))
            {
                m_ViewGuard = physicsHit.collider.gameObject;
                animator.SetTrigger("T_Flee");
            }
        }
        else
        {
            Debug.DrawRay(m_Thief.transform.position + Vector3.up, m_Thief.transform.TransformDirection(Vector3.forward) * 10, Color.green);
        }
    }

    public void GotoNextPoint(int addDestination)
    {
        if (m_Points.Length == 0)
            return;
        m_Agent.destination = m_Points[m_DestPoint].position;
        m_DestPoint = (m_DestPoint + addDestination) % m_Points.Length;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
    }
    public IEnumerator Wait()
    {
        m_Wait = true;
        yield return new WaitForSeconds(0.5f);
        m_Wait= false;
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
