using UnityEngine;

public class EvilDoctorController : Boss
{
    [SerializeField] private int damageReceivedRequiredForTeleportation;
    [SerializeField] private VignetteController vignetteController;
    [SerializeField] private PlayerSpawner playerSpawner;

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

        StartFight();
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
        weapon.Attack(playerSpawner.Player.gameObject);
        isAttack = false;
    }

    private void Teleport()
    {
        teleport.StartTeleportTimer();
        teleport.Teleport(teleport.ChoosePointForTeleportation());
        hitPointsAfterTeleportation = currentHitPoints;
        isTeleporting = false;
        vignetteController.EnableVignette(transform.position, playerSpawner.Player.transform.position);
    }

    private void CheckReceiveDamage(int hitPoints)
    {
        currentHitPoints = hitPoints;
        int damage = hitPointsAfterTeleportation - currentHitPoints;

        if (damage >= damageReceivedRequiredForTeleportation)
        {
            Teleport();
        }
    }
}
