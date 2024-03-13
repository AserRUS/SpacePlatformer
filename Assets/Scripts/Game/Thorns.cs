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
            New_ShieldController shieldController = collision.gameObject.GetComponent<New_ShieldController>();
            if (shieldController)
            {
                New_Shield shield = shieldController.GetShield();
                if (shield)
                {
                    shield.RemoveHitpoints(m_Damage, shield.gameObject);
                }
                else
                {
                    player.RemoveHitpoints(m_Damage, player.gameObject);

                }
            }
        }

        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((Vector2)(collision.transform.position - transform.position).normalized * m_ImactForce, ForceMode.Impulse);
        }

        PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Stun();
        }
    }
}
