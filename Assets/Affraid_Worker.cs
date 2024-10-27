using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class Affraid_Worker : StateMachineBehaviour
{
    private GameObject m_Worker;
    public Transform[] m_Points;
    private NavMeshAgent m_Agent;
    public int m_DestPoint = 0;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Worker = animator.gameObject;
        m_Points = m_Worker.GetComponent<AIData_Worker>().points;
        m_Agent = m_Worker.GetComponent<NavMeshAgent>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < 10; i++)
        {
            // Calcular el ¨¢ngulo para cada rayo
            float angle = i * (360f / 10);
            float radians = angle * Mathf.Deg2Rad;

            // Calcular la direcci¨®n en el plano XZ
            Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));

            // Realizar el Raycast en la direcci¨®n calculada
            RaycastHit hit;
            if (Physics.Raycast(m_Worker.transform.position, direction, out hit, 5))
            {
                // Si golpea algo, dibujar el rayo en rojo
                Debug.DrawRay(m_Worker.transform.position, direction * hit.distance, Color.red);
            }
            else
            {
                // Si no golpea nada, dibujar el rayo completo en verde
                Debug.DrawRay(m_Worker.transform.position, direction * 5, Color.green);
            }
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
