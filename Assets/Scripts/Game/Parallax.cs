using UnityEngine;



public class Parallax : MonoBehaviour
{
        
    [SerializeField] private float m_ParallaxPower_x;
    [SerializeField] private float m_ParallaxPower_y;

    private float m_LastCameraX;
    private float m_LastCameraY;

    private void Awake()
    {
        m_LastCameraX = Camera.main.transform.position.x;
        m_LastCameraY = Camera.main.transform.position.y;
    }
    public void ParallaxUpdate()
    {
        float deltaX = Camera.main.transform.position.x - m_LastCameraX;
        float deltaY = Camera.main.transform.position.y - m_LastCameraY;
        transform.position += Vector3.right * (deltaX * m_ParallaxPower_x) + Vector3.up * (deltaY * m_ParallaxPower_y);
        m_LastCameraX = Camera.main.transform.position.x;
        m_LastCameraY = Camera.main.transform.position.y;
    }
}

