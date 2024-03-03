using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class EvilDoctorTeleport : MonoBehaviour
{
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private Transform[] teleportationPoints;
    [SerializeField] private float timeBetweenTeleportation;

    private Vector3 lastTeleportPoint;
    private bool readyForTeleport;

    public bool ReadyForTeleport => readyForTeleport;

    private void Start()
    {
        readyForTeleport = false;

        Teleport(teleportationPoints[0].position);
        lastTeleportPoint = teleportationPoints[0].position;
    }

    public void StartTeleportTimer()
    {
        StartCoroutine(PreparationForTeleport(timeBetweenTeleportation));
    }

    public Vector3 ChoosePointForTeleportation()
    {
        int randomPoint = Random.Range(0, teleportationPoints.Length);

        while (teleportationPoints[randomPoint].position == lastTeleportPoint)
        {
            randomPoint = Random.Range(0, teleportationPoints.Length);
        }

        lastTeleportPoint = teleportationPoints[randomPoint].position;
        return lastTeleportPoint;
    }

    public void Teleport(Vector3 position)
    {
        if (teleportEffect)
            Instantiate(teleportEffect, transform.position, Quaternion.identity);

        transform.position = position;
        readyForTeleport = false;
        StartTeleportTimer();
    }

    private IEnumerator PreparationForTeleport(float time)
    {
        yield return new WaitForSeconds(time);
        readyForTeleport = true;
    }
}
