using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_ImactForce;

    private void OnCollisionEnter(Collision collision)
    {
        Destructible dest = collision.transform.GetComponent<Destructible>();
        if (dest != null)
        {
            dest.RemoveHitpoints(m_Damage, gameObject);

            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce((Vector2)(collision.transform.position - transform.position).normalized * m_ImactForce, ForceMode.Impulse);
            }
        }

        
    }
}
