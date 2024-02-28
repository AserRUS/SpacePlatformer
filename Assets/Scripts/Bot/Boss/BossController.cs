using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class BossController : Boss
{
    [Header("Teleport")]
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private Transform[] teleportationPoints;
    [SerializeField] private float timeBetweenTeleportation;
    [SerializeField] private int damageReceivedRequiredForTeleportation;

    private bool readyForTeleport;
    private int hitPointsAfterTeleportation;
    private int currentHitPoints;

    protected override void Start()
    {
        base.Start();
        readyForTeleport = false;

        currentHitPoints = destructible.MaxHitPoints;
        destructible.HitPointChangeEvent += CheckReceiveDamage;
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
    }

    private Vector3 ChoosePointForTeleportation()
    {
        int randomPoint = Random.Range(0, teleportationPoints.Length - 1);

        while (teleportationPoints[randomPoint].position == transform.position)
        {
            randomPoint = Random.Range(0, teleportationPoints.Length - 1);
        }

        return teleportationPoints[randomPoint].position;
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
