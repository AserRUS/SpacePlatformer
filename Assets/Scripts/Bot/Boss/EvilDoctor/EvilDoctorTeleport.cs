using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class EvilDoctorTeleport : MonoBehaviour
{
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private Bunker[] bunkers;
    [SerializeField] private float timeBetweenTeleportation;

    private Bunker lastTeleportBunker;
    private bool readyForTeleport = false;
    private Coroutine preparationForTeleportCoroutine;

    public bool ReadyForTeleport => readyForTeleport;

    private void Start()
    {
        lastTeleportBunker = bunkers[0];
        Teleport(lastTeleportBunker.TeleportPoint.position);
        bunkers[0].SetShieldActive(true);
    }
    
    public void StartTeleportTimer()
    {
        if (preparationForTeleportCoroutine != null)
            StopCoroutine(preparationForTeleportCoroutine);

        preparationForTeleportCoroutine = StartCoroutine(PreparationForTeleport(timeBetweenTeleportation));
    }

    public void StopTeleportTimer()
    {
        if (preparationForTeleportCoroutine != null)
            StopCoroutine(preparationForTeleportCoroutine);
    }

    public Vector3 ChoosePointForTeleportation()
    {
        lastTeleportBunker.SetShieldActive(false);
        int randomPoint = Random.Range(0, bunkers.Length);

        while (bunkers[randomPoint].TeleportPoint.position == lastTeleportBunker.TeleportPoint.position)
        {
            randomPoint = Random.Range(0, bunkers.Length);
        }

        lastTeleportBunker = bunkers[randomPoint];
        lastTeleportBunker.SetShieldActive(true);
        return lastTeleportBunker.TeleportPoint.position;
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
        float starttime = Time.realtimeSinceStartup;
        yield return new WaitForSeconds(time);
        readyForTeleport = true;
    }

    public void SetReadyForTeleport()
    {
        readyForTeleport = true;
    }
}
