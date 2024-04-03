using UnityEngine;
using UnityEngine.Events;

public class EvilDoctorController : Boss
{
    public event UnityAction OnDamageReceivedForShield;

    [SerializeField] private int damageReceivedRequiredForTeleportation;
    [SerializeField] private int damageReceivedRequiredForShield;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private UIWarning uiWarning;

    private EvilDoctorWeapon weapon;
    private EvilDoctorTeleport teleport;
    private EvilDoctorAnimationController animationController;

    private int hitPointsAfterTeleportation;
    private int currentHitPoints;
    private bool isTeleporting;
    private bool isAttack;
    private bool isActive;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<EvilDoctorWeapon>();
        teleport = GetComponent<EvilDoctorTeleport>();
        animationController = GetComponentInChildren<EvilDoctorAnimationController>();
        animationController.OnEndAttackAnim += Attack;
        animationController.OnEndTeleportAnim += Teleport;
        destructible.HitPointChangeEvent += CheckReceiveDamage;
    }

    private void Update()
    {
        ChooseBehavior();
    }

    private void OnDestroy()
    {
        animationController.OnEndAttackAnim -= Attack;
        animationController.OnEndTeleportAnim -= Teleport;

        destructible.HitPointChangeEvent -= CheckReceiveDamage;
    }

    private void ChooseBehavior()
    {
        if (isAttack) return;
        if (isTeleporting) return;

        if (teleport.ReadyForTeleport)
        {
            TurnOnTeleportAnim();
        }

        if (weapon.ReadyForAttack)
            TurnOnAttackAnim();
    }

    public override void StartFight()
    {
        if (weapon == null)
            weapon = GetComponent<EvilDoctorWeapon>();
        if (teleport == null)
            teleport = GetComponent<EvilDoctorTeleport>();

        weapon.StartAttackTimer();

        if (destructible == null)
            destructible = GetComponent<Destructible>();

        currentHitPoints = destructible.MaxHitPoints;
        hitPointsAfterTeleportation = currentHitPoints;
        teleport.StartTeleportTimer();
        isActive = true;
    }
    
    private void TurnOnAttackAnim()
    {
        animationController.TurnOnAnimAttack();
        isAttack = true;
    }

    private void TurnOnTeleportAnim()
    {
        animationController.TurnOnTeleportAnimAttack();
        isTeleporting = true;
    }

    private void Attack()
    {
        weapon.Attack(playerSpawner.GetPlayer().gameObject);
        isAttack = false;
    }

    private void Teleport()
    {
        teleport.Teleport(teleport.ChoosePointForTeleportation());
        hitPointsAfterTeleportation = currentHitPoints;
        isTeleporting = false;
        uiWarning.EnableWarning(transform.position, playerSpawner.GetPlayer().transform.position);
    }

    private void CheckReceiveDamage(int hitPoints)
    {
        if (!isActive) return;

        currentHitPoints = hitPoints;
        int damage = Mathf.Abs(hitPointsAfterTeleportation - currentHitPoints);

        if (damage >= damageReceivedRequiredForTeleportation)
        {
            teleport.StopTeleportTimer();
            teleport.SetReadyForTeleport();
        }
        else if (damage >= damageReceivedRequiredForShield)
        {
            OnDamageReceivedForShield?.Invoke();
        }
    }

    public override void OnPlayerDeath()
    {
        destructible.AddHitpoints(destructible.MaxHitPoints);
        currentHitPoints = destructible.MaxHitPoints;
        teleport.StopTeleportTimer();
        weapon.StopAttackTimer();
        isTeleporting = false;
        isAttack = false;
        isActive = false;
    }
}
