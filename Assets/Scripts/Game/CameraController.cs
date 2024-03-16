using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    private enum CameraMode
    {
        FollowMode,
        FlightMode
    }

    public event UnityAction FlightFinishEvent;

    [SerializeField] private CameraMode m_CameraMode;

    [SerializeField] private Vector2 m_CameraBorderX;
    [SerializeField] private Vector2 m_CameraBorderY;
    [SerializeField] private Parallax[] m_Parallax;

    [Header("Follow mode")]
    [SerializeField] private Vector2 m_SpeedFollow;
    

    [Header("Flight mode")]
    [SerializeField] private Vector2 m_FlightSpeed;
    [SerializeField] private ParameterCurve m_SpeedCurve;

    private Transform cameraFollowTarget;
    private Transform cameraFlightTarget;
    private Transform cameraStartPosition;
    public void SetCameraFollowTarget(Transform target)
    {
        this.cameraFollowTarget = target;
    }

    public void SetFollowMode()
    {
        m_CameraMode = CameraMode.FollowMode;
    }
    public void SetFlightMode(Transform cameraStartPosition, Transform cameraFlightTarget)
    {        
        this.cameraStartPosition = cameraStartPosition;
        transform.position = cameraStartPosition.position;

        this.cameraFlightTarget = cameraFlightTarget;

        m_CameraMode = CameraMode.FlightMode;
    }

    private void LateUpdate()
    {        
        if (cameraFollowTarget == null) return;

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        float camPosX = 0;
        float camPosY = 0;

        if (m_CameraMode == CameraMode.FollowMode)
        { 
            float camSpeedX = m_SpeedFollow.x * Time.deltaTime;
            float camSpeedY = m_SpeedFollow.y * Time.deltaTime;

            camPosX = Mathf.Lerp(transform.position.x, cameraFollowTarget.position.x, camSpeedX);
            camPosY = Mathf.Lerp(transform.position.y, cameraFollowTarget.position.y, camSpeedY);
        }
        else if (m_CameraMode == CameraMode.FlightMode)
        {
            if (transform.position == cameraFlightTarget.position)
            {
                FlightFinishEvent?.Invoke();
            }

            float camSpeedX = m_SpeedCurve.GetValueBetween(cameraStartPosition.position.x, cameraFlightTarget.position.x, transform.position.x) * m_FlightSpeed.x * Time.deltaTime;
            float camSpeedy = m_SpeedCurve.GetValueBetween(cameraStartPosition.position.y, cameraFlightTarget.position.y, transform.position.y) * m_FlightSpeed.y * Time.deltaTime;
            camPosX = Mathf.MoveTowards(transform.position.x, cameraFlightTarget.position.x, camSpeedX);
            camPosY = Mathf.MoveTowards(transform.position.y, cameraFlightTarget.position.y, camSpeedy);
        }
        
        

        Vector3 newCamPos = new Vector3(Mathf.Clamp(camPosX, m_CameraBorderX.x + width / 2, m_CameraBorderX.y - width / 2), Mathf.Clamp(camPosY, m_CameraBorderY.x + height / 2, m_CameraBorderY.y - height / 2), transform.position.z);
        transform.position = newCamPos;
        if (m_Parallax != null)
        {
            for (int i = 0; i < m_Parallax.Length; i++)
            {
                m_Parallax[i].ParallaxUpdate();
            }
        }
            
    }    
    

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {          
        
        Gizmos.DrawLine(new Vector3(m_CameraBorderX.x, -10, 0), new Vector3(m_CameraBorderX.x, 10, 0));

        Gizmos.DrawLine(new Vector3(m_CameraBorderX.y, -10, 0), new Vector3(m_CameraBorderX.y, 10, 0));

        Gizmos.DrawLine(new Vector3(-10, m_CameraBorderY.y, 0), new Vector3(10, m_CameraBorderY.y, 0));

        Gizmos.DrawLine(new Vector3(-10, m_CameraBorderY.x, 0), new Vector3(10, m_CameraBorderY.x, 0));
    }

#endif
}
