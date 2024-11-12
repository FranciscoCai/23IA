using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Flee_Worker : StateMachineBehaviour
{
    private GameObject m_Worker;
    public Transform[] m_groupingsPoints;
    private NavMeshAgent m_Agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Worker = animator.gameObject;
        m_groupingsPoints = m_Worker.GetComponent<AIData_Worker>().groupingsPoints;
        m_Agent = m_Worker.GetComponent<NavMeshAgent>();
        Vector3 destination = AIDirector.instance.WorkerClosePoint(m_Worker.transform, AIDirector.instance.A_thiefHidePoints);
        m_Agent.SetDestination(destination);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
        {
            animator.SetTrigger("T_Affraid");
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
