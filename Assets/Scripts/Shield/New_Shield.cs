using System.Collections;
using UnityEngine;

public class New_Shield : Destructible
{
    [SerializeField] private float lifeTime;
    [Range(0, 1)]
    [SerializeField] private float minTransparent = 0.1f;
    [Range(0, 1)]
    [SerializeField] private float maxTransparent = 0.8f;

    private New_Shield shield;
    private Renderer shieldRenderer;
    private Coroutine lifeTimerCoroutine;

    protected override void Start()
    {
        base.Start();

        shield = GetComponent<New_Shield>();
        shieldRenderer = GetComponent<Renderer>();

        shield.HitPointChangeEvent += ChangeTransparent;
        shield.RemoveHitpoints(MaxHitPoints - 1, shield.gameObject);
    }

    private void OnDestroy()
    {
        shield.HitPointChangeEvent -= ChangeTransparent;
    }

    private void ChangeTransparent(int hp)
    {
        float percentageOfHitPoints = hp * 100 / MaxHitPoints;
        float valueOfTransparent = ((maxTransparent - minTransparent) * percentageOfHitPoints / 100) + minTransparent;

        Color color = shieldRenderer.material.color;
        color.a = valueOfTransparent;
        shieldRenderer.material.color = color;
    }

    public void ResetLifetime()
    {
        if (lifeTimerCoroutine != null)
            StopCoroutine(lifeTimerCoroutine);

        lifeTimerCoroutine = StartCoroutine(LifeTimerCoroutine(lifeTime));
    }

    private IEnumerator LifeTimerCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        shield.RemoveHitpoints(shield.MaxHitPoints, gameObject);
    }
}
