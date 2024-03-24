using System.Collections;
using UnityEngine;

public class EvilDoctorWeapon : MonoBehaviour
{
    [SerializeField] private EvilDoctorAttackZone attackZone;
    [SerializeField] private float timeBetweenAttack;

    private Coroutine waitTimeBetweenAttackCoroutine;
    private bool readyForAttack;

    public bool ReadyForAttack => readyForAttack;

    private void Start()
    {
        StartAttackTimer();
    }

    public void Attack(GameObject player)
    {
        if (!readyForAttack) return;

        attackZone.Attack(player);
        readyForAttack = false;
        StartAttackTimer();
    }

    public void StartAttackTimer()
    {
        if (waitTimeBetweenAttackCoroutine != null)
            StopCoroutine(waitTimeBetweenAttackCoroutine);
        waitTimeBetweenAttackCoroutine = StartCoroutine(WaitTimeBetweenAttack(timeBetweenAttack));
    }

    public void StopAttackTimer()
    {
        if (waitTimeBetweenAttackCoroutine != null)
            StopCoroutine(waitTimeBetweenAttackCoroutine);
    }

    private IEnumerator WaitTimeBetweenAttack(float time)
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        readyForAttack = true;
    }
}
