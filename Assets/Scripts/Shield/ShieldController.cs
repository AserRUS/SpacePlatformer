using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Storage energyStorage;
    [SerializeField] private Shield shieldPrefab;
    [SerializeField] private ShieldPropertes shieldPropertes;

    private Player player;
    private Shield shield;
    private bool isShieldActive;
    private bool buttonClamp;
    private float time;
    private float timeStep = 0.1f;


    private void Start()
    {
        player = GetComponent<Player>();
        isShieldActive = false;
        player.DeathEvent += OnPlayerDeath;
        
    }
    private void Update()
    {
        ButtonClamp();
    }

    private void ButtonClamp()
    {
        if (!buttonClamp) return;
        if (!isShieldActive) return;

        if (time >= timeStep)
        {
            time = 0;
            shield.AddHitpoints(shieldPropertes.HitPointsInPeroidOfTime);
            energyStorage.RemoveValue(shieldPropertes.RequiredEnergyInPeroidOfTime);
        }

        time += Time.deltaTime;
    }

    public void IncreaseShield()
    {
        if (!isShieldActive)
        {
            shield = Instantiate(shieldPrefab, player.transform.position,
                Quaternion.identity, player.transform).GetComponent<Shield>();
            shield.DeathEvent += OnShieldBroke;
            player.SetInvulnerable(true);
        }

        shield.ResetLifetime();
        buttonClamp = true;
        isShieldActive = true;
    }

    public void StopShieldIncrease()
    {
        buttonClamp = false;
        shield.ResetLifetime();
    }

    private void OnShieldBroke()
    {
        shield.DeathEvent -= OnShieldBroke;
        shield = null;
        isShieldActive = false;
        player.SetInvulnerable(false);
    }

    private void OnPlayerDeath()
    {
        player.DeathEvent -= OnPlayerDeath;
        if (shield)
            shield.RemoveHitpoints(shield.MaxHitPoints, shield.gameObject);
    }

    public Shield GetShield()
    {
        return shield;
    }
}
