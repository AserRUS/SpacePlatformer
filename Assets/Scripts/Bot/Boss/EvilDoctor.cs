using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class EvilDoctor : Boss
{
    [SerializeField] private EvilDoctorWeapon weapon;
    [Header("Teleport")]
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private Transform[] teleportationPoints;
    [SerializeField] private float timeBetweenTeleportation;
    [SerializeField] private int damageReceivedRequiredForTeleportation;

    private Vector3 lastTeleportPoint;
    private bool readyForTeleport;
    private int hitPointsAfterTeleportation;
    private int currentHitPoints;

    protected override void Start()
    {
        base.Start();
        readyForTeleport = false;

        currentHitPoints = destructible.MaxHitPoints;
        destructible.HitPointChangeEvent += CheckReceiveDamage;

        Teleport(teleportationPoints[0].position);
        lastTeleportPoint = teleportationPoints[0].position;
    }

    private void Update()
    {
        if (readyForTeleport)
            Teleport(ChoosePointForTeleportation());
    }

    private void OnDestroy()
    {
        destructible.HitPointChangeEvent -= CheckReceiveDamage;
    }

    public override void StartFight()
    {
        hitPointsAfterTeleportation = currentHitPoints;
        StartCoroutine(PreparationForTeleport(timeBetweenTeleportation));
        weapon.StartAttackTimer();
    }

    private Vector3 ChoosePointForTeleportation()
    {
        int randomPoint = Random.Range(0, teleportationPoints.Length);

        while (teleportationPoints[randomPoint].position == lastTeleportPoint)
        {
            randomPoint = Random.Range(0, teleportationPoints.Length);
        }

        lastTeleportPoint = teleportationPoints[randomPoint].position;
        return lastTeleportPoint;
    }

    private void Teleport(Vector3 position)
    {
        if (teleportEffect)
            Instantiate(teleportEffect, transform.position, Quaternion.identity);

        transform.position = position;
        readyForTeleport = false;
        hitPointsAfterTeleportation = currentHitPoints;
        StartCoroutine(PreparationForTeleport(timeBetweenTeleportation));
    }

    private IEnumerator PreparationForTeleport(float time)
    {
        yield return new WaitForSeconds(time);
        readyForTeleport = true;
    }

    private void CheckReceiveDamage(int hitPoints)
    {
        currentHitPoints = hitPoints;
        int damage = hitPointsAfterTeleportation - currentHitPoints;

        if (damage >= damageReceivedRequiredForTeleportation)
        {
            StopCoroutine(PreparationForTeleport(0));
            readyForTeleport = true;
        }
    }
}
