using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class Affraid_Worker : StateMachineBehaviour
{
    private GameObject m_Worker;
    private NavMeshAgent m_Agent;
    [SerializeField] float m_RaycastDistance = 5f;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Worker = animator.gameObject;
        m_Agent = m_Worker.GetComponent<NavMeshAgent>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit[] hits = Physics.SphereCastAll(m_Worker.transform.position, m_RaycastDistance, Vector3.up,0.01f);
        if (hits == null)
        {
            return;
        }

        for (int i = 0; i < hits.Length; i++) 
        {
           if( hits[i].collider.CompareTag("Guard"))
            {
                animator.SetTrigger("T_Posing");
            }

        }
        //{
        //    float angle = i * (360f / m_RaycastNumber);
        //    float radians = angle * Mathf.Deg2Rad;

        //    Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));

        //    RaycastHit hit;
        //    if (Physics.Raycast(m_Worker.transform.position+Vector3.up, direction, out hit, m_RaycastDIstance))
        //    {
        //        Debug.DrawRay(m_Worker.transform.position+ Vector3.up, direction * hit.distance, Color.red);
        //        if(hit.collider.CompareTag("Guard"))
        //        {
        //            if ((animator.GetBehaviours<Posing_Worker>()[0].m_DestPoint - 1) < 0)
        //            {
        //                animator.GetBehaviours<Posing_Worker>()[0].m_DestPoint = animator.GetBehaviours<Posing_Worker>()[0].m_Points.Length - 1;
        //            }
        //            else
        //            {
        //                animator.GetBehaviours<Posing_Worker>()[0].m_DestPoint -= 1;
        //            }
        //            animator.SetTrigger("T_Posing");
        //        }
        //    }
        //    else
        //    {
        //        Debug.DrawRay(m_Worker.transform.position+ Vector3.up, direction * m_RaycastDIstance, Color.green);
        //    }
        //}
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
