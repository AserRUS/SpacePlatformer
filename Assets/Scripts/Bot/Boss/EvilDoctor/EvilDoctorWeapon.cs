using System.Collections;
using UnityEngine;

public class EvilDoctorWeapon : MonoBehaviour
{
    [SerializeField] private EvilDoctorAttackZone attackZone;
    [SerializeField] private float timeBetweenAttack;

    private bool readyForAttack;

    public bool ReadyForAttack => readyForAttack;

    public void Attack()
    {
        if (!readyForAttack) return;

        attackZone.Attack();
        readyForAttack = false;
        StartAttackTimer();
    }

    private IEnumerator WaitTimeBetweenAttack(float time)
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        readyForAttack = true;
    }

    public void StartAttackTimer()
    {
        StartCoroutine(WaitTimeBetweenAttack(timeBetweenAttack));
    }
}
