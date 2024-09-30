using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public bool TTimeScale;
    public float timeScale;
    void Start()
    {
        if(TTimeScale == true)
        {
            Time.timeScale = timeScale;
        }
    }
}
