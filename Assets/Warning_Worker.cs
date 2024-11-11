using UnityEngine;

public class Warning_Worker : StateMachineBehaviour
{
    private GameObject m_Worker;
    private UnityEngine.AI.NavMeshAgent m_Agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Worker = animator.gameObject;
        m_Agent = m_Worker.GetComponent<UnityEngine.AI.NavMeshAgent>();
        float distance = 0;
        //aidirctor.instance.closest(animator.trans.pos);
        Vector3 destination = Vector3.zero;
        for (int i = 0; i < AIDirector.instance.A_alarmTransform.Length; i++)
        {
            float actualDistance = Vector3.Distance(AIDirector.instance.A_alarmTransform[i].transform.position, m_Worker.transform.position);
            if (distance == 0)
            {
                distance = actualDistance;
                destination = AIDirector.instance.A_alarmTransform[i].transform.position;
            }
            else if (actualDistance <= distance)
            {
                distance = actualDistance;
                destination = AIDirector.instance.A_alarmTransform[i].transform.position;
            }
        }
        m_Agent.SetDestination(destination);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
        {
            AIDirector.instance.AlarmEfect();
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
