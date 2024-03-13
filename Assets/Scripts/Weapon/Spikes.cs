using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("With Shield")]
    [SerializeField] private int damageInPeroidOfTime;
    [SerializeField] private float timeStep = 0.1f;

    [Header("Without Shield")]
    [SerializeField] private int damage;
    [SerializeField] private float imactForce;

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
            rb.AddForce((Vector2)(collider.transform.position - transform.position).normalized * imactForce, ForceMode.Impulse);
        }

        PlayerMovement playerMovement = collider.transform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Stun();
        }
    }
}
