using System.Collections;
using UnityEngine;

public class Shield : Destructible
{
    [SerializeField] private float lifeTime;
    [SerializeField] private int requiredEnergy;

    private Shield shield;

    public int RequiredEnergy => requiredEnergy;

    protected override void Start()
    {
        base.Start();
        shield = GetComponent<Shield>();
        StartCoroutine(LifeTimer(lifeTime));
    }

    IEnumerator LifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        shield.RemoveHitpoints(shield.MaxHitPoints, gameObject);
    }

}
