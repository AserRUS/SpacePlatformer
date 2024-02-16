using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject m_SelectionEffect;

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Amplitude;
    [SerializeField] private float m_RotateSpeed;
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();
        if (player != null)
        {
            LevelProgress.Instance.AddStar();
            Instantiate(m_SelectionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
          
        }

    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(m_Speed * Time.time) * m_Amplitude, transform.position.z);
        transform.Rotate(0, 1 * m_RotateSpeed * Time.deltaTime, 0);
    }
}
