using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Destructible weakShieldPrefab;
    [SerializeField] private Destructible strongShieldPrefab;

    private Player player;
    private Destructible shield;
    private float timeLimitForStrongShield = 1f;
    private bool shieldActive;

    private void Start()
    {
        player = GetComponent<Player>();
        shieldActive = false;
        player.DeathEvent += OnPlayerDeath;
    }

    private void Update()
    {
        MoveShield();
    }

    private void MoveShield()
    {
        if (shieldActive)
        {
            shield.transform.position = player.transform.position;
        }
    }

    public void UseShield(float timeClamp)
    {
        if (shieldActive) return;

        if (timeClamp > timeLimitForStrongShield)
        {
            shield = Instantiate(strongShieldPrefab, player.transform.position, 
                Quaternion.identity).GetComponent<Destructible>();
        }
        else
        {
            shield = Instantiate(weakShieldPrefab, player.transform.position, 
                Quaternion.identity).GetComponent<Destructible>();
        }

        shieldActive = true;
        shield.DeathEvent += OnShieldBroke;
    }

    private void OnShieldBroke()
    {
        shield.DeathEvent -= OnShieldBroke;
        shieldActive = false;
    }

    private void OnPlayerDeath()
    {
        player.DeathEvent -= OnPlayerDeath;
        if (shield)
            shield.RemoveHitpoints(shield.MaxHitPoints, shield.gameObject);
    }
}
