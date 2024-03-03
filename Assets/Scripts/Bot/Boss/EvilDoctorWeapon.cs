using System.Collections;
using UnityEngine;

public class EvilDoctorWeapon : MonoBehaviour
{
    [SerializeField] private EvilDoctorAttackZone attackZone;
    [SerializeField] private float timeBetweenAttack;

    private bool attackAvailable;

    private void Update()
    {
        //temporary
        Attack();
    }

    private void Attack()
    {
        if (!attackAvailable) return;

        attackZone.Attack();
        attackAvailable = false;
        StartAttackTimer();
    }

    private IEnumerator WaitTimeBetweenAttack(float time)
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        attackAvailable = true;
    }

    public void StartAttackTimer()
    {
        StartCoroutine(WaitTimeBetweenAttack(timeBetweenAttack));
    }
}
