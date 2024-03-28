using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("With Shield")]
    [SerializeField] private int damageInPeroidOfTime;
    [SerializeField] private float timeStep = 0.1f;

    [Header("Without Shield")]
    [SerializeField] private int damage;
    [SerializeField] private float impactForce;

    private float time;
    private Player player;
    private New_ShieldController shieldController;

    private void OnTriggerEnter(Collider other)
    {
        player = other.transform.GetComponent<Player>();
        shieldController = other.GetComponent<New_ShieldController>();

        time = timeStep;
    }

    private void OnTriggerStay(Collider other)
    {
        if (player)
        {
            New_Shield shield = shieldController.GetShield();
            time += Time.deltaTime;

            if (time >= timeStep)
            {
                time = 0;
                
                if (shield)
                {
                    shield.RemoveHitpoints(damageInPeroidOfTime, shield.gameObject);
                }
                else
                {
                    player.RemoveHitpoints(damage, player.gameObject);

                    PushPlayer(other);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        time = 0;
        player = null;
        shieldController = null;
    }

    private void PushPlayer(Collider collider)
    {
        Rigidbody rb = collider.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;

            Vector3 direction = collider.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            if (angle >= 90)
            {
                rb.AddForce(new Vector2(-1, 1) * impactForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(1, 1) * impactForce, ForceMode.Impulse);
            }
        }

        PlayerMovement playerMovement = collider.transform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Stun();
        }
    }
}
