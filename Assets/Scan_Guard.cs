using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Scan_Guard : StateMachineBehaviour
{
    private GameObject m_Guard;
    private NavMeshAgent m_Agent;
    public int m_DestPoint = 0;
    [SerializeField] private float m_Duration;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Guard = animator.gameObject;
        m_Agent = m_Guard.GetComponent<NavMeshAgent>();
        animator.GetComponent<AIData_Guard>().StartCoroutine(Rotate_Guard(animator));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public IEnumerator Rotate_Guard(Animator animator)
    {
        float startRotation = m_Guard.transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < m_Duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / m_Duration) % 360.0f;
            m_Guard.transform.eulerAngles = new Vector3(m_Guard.transform.eulerAngles.x, yRotation, m_Guard.transform.eulerAngles.z);
            RaycastHit physicsHit;
            if (Physics.Raycast(m_Guard.transform.position + Vector3.up, m_Guard.transform.forward, out physicsHit, 10f))
            {
                Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.red);
                if (physicsHit.collider.gameObject.CompareTag("Thief"))
                {
                    animator.SetTrigger("T_Pursue");
                    yield break;
                }
                else
                {

                }
            }
            else
            {
                Debug.DrawRay(m_Guard.transform.position + Vector3.up, m_Guard.transform.TransformDirection(Vector3.forward) * 10, Color.green);
            }
            yield return null;
        }
        animator.SetTrigger("T_Patrol");
    }
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

