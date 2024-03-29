using UnityEngine;

public class EvilDoctorController : Boss
{
    [SerializeField] private int damageReceivedRequiredForTeleportation;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private UIWarning uiWarning;

    private EvilDoctorWeapon weapon;
    private EvilDoctorTeleport teleport;
    private EvilDoctorAnimationController animationController;

    private int hitPointsAfterTeleportation;
    private int currentHitPoints;
    private bool isTeleporting;
    private bool isAttack;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<EvilDoctorWeapon>();
        teleport = GetComponent<EvilDoctorTeleport>();
        animationController = GetComponentInChildren<EvilDoctorAnimationController>();
        animationController.OnEndAttackAnim += Attack;
        animationController.OnEndTeleportAnim += Teleport;

        currentHitPoints = destructible.MaxHitPoints;
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

        if (weapon.ReadyForAttack)
            TurnOnAttackAnim();

        if (teleport.ReadyForTeleport)
            TurnOnTeleportAnim();
    }

    public override void StartFight()
    {
        if (weapon == null)
            weapon = GetComponent<EvilDoctorWeapon>();
        if (teleport == null)
            teleport = GetComponent<EvilDoctorTeleport>();

        weapon.StartAttackTimer();

        hitPointsAfterTeleportation = currentHitPoints;
        teleport.StartTeleportTimer();
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
        teleport.StartTeleportTimer();
        teleport.Teleport(teleport.ChoosePointForTeleportation());
        hitPointsAfterTeleportation = currentHitPoints;
        isTeleporting = false;
        uiWarning.EnableWarning(transform.position, playerSpawner.GetPlayer().transform.position);
    }

    private void CheckReceiveDamage(int hitPoints)
    {
        currentHitPoints = hitPoints;
        int damage = Mathf.Abs(hitPointsAfterTeleportation - currentHitPoints);

        if (damage >= damageReceivedRequiredForTeleportation)
        {
            teleport.StopTeleportTimer();
            teleport.SetReadyForTeleport();
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
    }
}
