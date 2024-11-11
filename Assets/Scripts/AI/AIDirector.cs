using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class AIDirector : MonoBehaviour
{
    public static AIDirector instance;
    public Transform[] A_alarmTransform;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    public void AlarmEfect()
    {
        GameObject[] worker = GameObject.FindGameObjectsWithTag("Worker");
        foreach (GameObject worker2 in worker)
        {
            worker2.GetComponent<Animator>().SetTrigger("T_Alarm");
        }
    }
}
