using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;
    [SerializeField] private float m_CameraSpeedX;
    [SerializeField] private float m_CameraSpeedY;
    [SerializeField] private Vector2 m_CameraBorderX;
    [SerializeField] private Vector2 m_CameraBorderY;
    [SerializeField] private Parallax m_Parallax;    


    private void LateUpdate()
    {        
        if (m_Target == null) return;

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;        

        float camPosX = Mathf.Lerp(transform.position.x, m_Target.transform.position.x, m_CameraSpeedX * Time.deltaTime);
        float camPosY = Mathf.Lerp(transform.position.y, m_Target.transform.position.y, m_CameraSpeedY * Time.deltaTime);
        Vector3 newCamPos = new Vector3(Mathf.Clamp(camPosX, m_CameraBorderX.x + width / 2, m_CameraBorderX.y - width / 2), Mathf.Clamp(camPosY, m_CameraBorderY.x + height / 2, m_CameraBorderY.y - height / 2), transform.position.z);
        transform.position = newCamPos;
        if (m_Parallax != null)
            m_Parallax.ParallaxUpdate();
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
