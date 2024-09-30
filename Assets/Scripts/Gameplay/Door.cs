using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool Enabled = false;
    [SerializeField]
    public float DoorSpeed = 1f;

    private Coroutine m_RotationRoutine = null;
    private Transform m_Actor;
    private bool m_Opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!Enabled)
        {
            return;
        }

        if (m_RotationRoutine != null)
        {
            StopCoroutine(m_RotationRoutine);
        }
        m_Actor = other.gameObject.transform;
        m_RotationRoutine = StartCoroutine(DoorRotation());
    }

    private IEnumerator DoorRotation()
    {
        float currentAngle = transform.localEulerAngles.y;
        bool clockWise = m_Opened ? ((currentAngle % 360) >= 270) : ObjectIsBehindDoor();
        float targetAngle = m_Opened ? 0f : (clockWise ? 90f : 270f);
        float difference = Mathf.Abs(Mathf.DeltaAngle(targetAngle, currentAngle));
        float step = (DoorSpeed / 90f) * Time.fixedDeltaTime;
        currentAngle = 0;
        while (difference - currentAngle > 0)
        {
            currentAngle += step;
            transform.Rotate(0f, clockWise ? step : -step, 0f, Space.Self);
            yield return new WaitForFixedUpdate();
        }

        transform.localEulerAngles = new Vector3(0f, targetAngle, 0f);
        m_Opened = !m_Opened;
    }

    private bool ObjectIsBehindDoor()
    {
        Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
        Vector3 playerTransformDirection = m_Actor.position - transform.position;
        return Vector3.Dot(doorTransformDirection, playerTransformDirection) < 0;
    }
}
