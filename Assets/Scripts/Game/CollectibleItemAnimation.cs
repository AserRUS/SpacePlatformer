using UnityEngine;

public class CollectibleItemAnimation : MonoBehaviour
{
    [SerializeField] private Transform m_VisualModel;

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Amplitude;
    [SerializeField] private float m_RotateSpeed;
    [SerializeField] private Vector3 m_RotateDirection;

    
    

    private void Update()
    {
        m_VisualModel.position = new Vector3(m_VisualModel.position.x, transform.position.y + Mathf.Sin(m_Speed * Time.time) * m_Amplitude, m_VisualModel.position.z);
        m_VisualModel.transform.Rotate(m_RotateDirection * m_RotateSpeed * Time.deltaTime);
    }

}
