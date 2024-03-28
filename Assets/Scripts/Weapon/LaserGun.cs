using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LaserGun : MonoBehaviour
{
    public event UnityAction LaserActivateEvent;
    public event UnityAction LaserDeactivateEvent;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;

    [Header("Laser properties")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] private float maxLength;
    [SerializeField] private float timeLaserActivation;
    [SerializeField] private int minRequiredCartridge;
    [SerializeField] private int maxRequiredCartridge;
    [SerializeField] private float timeBetweenIncreaseLaserProperties;
    [SerializeField] private int countStepForIncreaseLaser;

    [Header("Damage")]
    [SerializeField] private int minDamageInPeroidOfTime;
    [SerializeField] private int maxDamageInPeroidOfTime;
    [SerializeField] private float timeBetweenDamage;

    [Header("ScriptableObjects")]
    [SerializeField] private ButtonPressDuration buttonPressDuration;

    private PlayerMovement playerMovement;
    private float width;
    private int damageInPeroidOfTime;
    private int requiredCartridge;
    private bool damageAvailable;
    private bool increaseAvailable;

    private int stepDamage;
    private int stepCartride;
    private float stepWidth;

    private Coroutine deactivateLasetCoroutine;
    private Coroutine reloadDamagaCoroutine;
    private Coroutine increaseLaserCoroutine;


    public bool LaserActive => lineRenderer.enabled;
    public bool LaserHasMaxDamage => damageInPeroidOfTime >= maxDamageInPeroidOfTime;
    public int RequiredCartridge => requiredCartridge;

    private void Start()
    {
        //Deactivate();

        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMovement.OnJumpEvent += Deactivate;
        playerMovement.OnRotationEvent += Deactivate;

        stepDamage = (int)Mathf.Ceil((float)(maxDamageInPeroidOfTime - minDamageInPeroidOfTime) / countStepForIncreaseLaser);
        stepCartride = (int)Mathf.Ceil((float)(maxRequiredCartridge - minRequiredCartridge) / countStepForIncreaseLaser);
        stepWidth = (maxWidth - minWidth) / countStepForIncreaseLaser;
    }

    private void FixedUpdate()
    {
        Laser();

    }

    private void OnDestroy()
    {
        playerMovement.OnJumpEvent -= Deactivate;
        playerMovement.OnRotationEvent -= Deactivate;
    }

    private void Laser()
    {
        if (!LaserActive) return;

        Ray ray = new Ray(firePoint.position, firePoint.right);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLength, layerMask);
        Vector3 hitPosition = cast ? hit.point : firePoint.position + firePoint.right * maxLength;

        if (hit.transform != null)
        {
            Destructible dest = hit.transform.GetComponent<Destructible>();
            if (dest)
                Damage(dest);
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPosition);
        particles.transform.position = hitPosition;
    }
    private void Damage(Destructible dest)
    {
        if (!damageAvailable) return;

        dest.RemoveHitpoints(damageInPeroidOfTime, gameObject);
        damageAvailable = false;

        if (reloadDamagaCoroutine != null)
            StopCoroutine(reloadDamagaCoroutine);
        reloadDamagaCoroutine = StartCoroutine(ReloadDamage(timeBetweenDamage));
    }

    public void Activate()
    {
        lineRenderer.enabled = true;
        LaserActivateEvent?.Invoke();
        damageAvailable = true; 
        particles.Play();
        increaseAvailable = false;


        if (increaseLaserCoroutine != null)
            StopCoroutine(increaseLaserCoroutine);
        increaseLaserCoroutine = StartCoroutine(IncreaseLaserTimer(timeBetweenIncreaseLaserProperties));

        if (deactivateLasetCoroutine != null)
            StopCoroutine(deactivateLasetCoroutine);
    }

    public void Deactivate()
    {
        if (!LaserActive) return;
        lineRenderer.enabled = false;
        LaserDeactivateEvent?.Invoke();
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.position);
        particles.Stop();

        width = minWidth;
        SetLaserWidth(width);
        damageInPeroidOfTime = minDamageInPeroidOfTime;
        requiredCartridge = minRequiredCartridge;

        if (increaseLaserCoroutine != null)
            StopCoroutine(increaseLaserCoroutine);

        if (reloadDamagaCoroutine != null)
            StopCoroutine(reloadDamagaCoroutine);


        if (deactivateLasetCoroutine != null)
            StopCoroutine(deactivateLasetCoroutine);
    }

    public void IncreaseLaser(float time)
    {
        if (!LaserActive) return;
        if (!increaseAvailable) return;
        if (time >= buttonPressDuration.MaxTimeButtonClamp) return;

        damageInPeroidOfTime += stepDamage;
        if (damageInPeroidOfTime > maxDamageInPeroidOfTime)
            damageInPeroidOfTime = maxDamageInPeroidOfTime;

        requiredCartridge += stepCartride;
        if (requiredCartridge > maxRequiredCartridge)
            requiredCartridge = maxRequiredCartridge;

        width += stepWidth;
        if (width > maxWidth)
            width = maxWidth;
        SetLaserWidth(width);

        increaseAvailable = false;
        
        if (increaseLaserCoroutine  != null)
            StopCoroutine(increaseLaserCoroutine);
        increaseLaserCoroutine = StartCoroutine(IncreaseLaserTimer(timeBetweenIncreaseLaserProperties));
    }

    public void StartDeactivateLaserTimer()
    {
        if (deactivateLasetCoroutine != null)
            StopCoroutine(deactivateLasetCoroutine);
        deactivateLasetCoroutine = StartCoroutine(DeactivateLaser(timeLaserActivation));
    }

    private  IEnumerator DeactivateLaser(float time)
    {
        yield return new WaitForSeconds(time);
        Deactivate();
    }

    private IEnumerator ReloadDamage(float time)
    {
        yield return new WaitForSeconds(time);
        damageAvailable = true;
    }

    private IEnumerator IncreaseLaserTimer(float time)
    {
        yield return new WaitForSeconds(time);
        increaseAvailable = true;
    }

    private void SetLaserWidth(float value)
    {
        lineRenderer.startWidth = value;
        lineRenderer.endWidth = value;
    }
}
