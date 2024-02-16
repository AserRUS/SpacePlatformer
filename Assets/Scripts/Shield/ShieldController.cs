using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Shield weakShieldPrefab;
    [SerializeField] private Shield strongShieldPrefab;

    private Player player;
    private Storage energyStorage;
    private Destructible shield;
    private float timeLimitForStrongShield = 1f;
    private bool shieldActive;

    private void Start()
    {
        player = GetComponent<Player>();
        energyStorage = GetComponent<Storage>();
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
            if (energyStorage.CurrentValue <= strongShieldPrefab.RequiredEnergy)
                return;

            energyStorage.RemoveValue(strongShieldPrefab.RequiredEnergy);

            shield = Instantiate(strongShieldPrefab, player.transform.position, 
                Quaternion.identity).GetComponent<Destructible>();
        }
        else
        {
            if (energyStorage.CurrentValue <= weakShieldPrefab.RequiredEnergy)
                return;

            energyStorage.RemoveValue(weakShieldPrefab.RequiredEnergy);

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
