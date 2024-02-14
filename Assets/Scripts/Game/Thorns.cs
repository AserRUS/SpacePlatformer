using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_ImactForce;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            player.RemoveHitpoints(m_Damage, gameObject);

            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce((Vector2)(collision.transform.position - transform.position).normalized * m_ImactForce, ForceMode.Impulse);
            }

            PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.Stun();
            }
        }

        
    }
}
