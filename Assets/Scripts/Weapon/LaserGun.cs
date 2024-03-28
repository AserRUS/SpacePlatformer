using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Transform firePoint;

    [Header("Laser properties")]
    [SerializeField] private float maxLength;
    [SerializeField] private float timeLaserActivation;
    [SerializeField] private int requiredCartridge;

    [Header("Damage")]
    [SerializeField] private int damageInPeroidOfTime;
    [SerializeField] private float timeBetweenDamage;

    private PlayerMovement playerMovement;
    private bool damageAvailable;

    public int RequiredCartridge => requiredCartridge;
    public bool LaserActive => lineRenderer.enabled;

    private void Start()
    {
        Deactivate();

        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMovement.OnJumpEvent += Deactivate;
        playerMovement.OnRotationEvent += Deactivate;
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
        if (!lineRenderer.enabled) return;

        Ray ray = new Ray(firePoint.position, firePoint.right);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLength);
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
        StartCoroutine(ReloadDamage(timeBetweenDamage));
    }
    
    public void Activate()
    {        
        lineRenderer.enabled = true;
        damageAvailable = true;
        particles.Play();
        StartCoroutine(DeactivateLaser(timeLaserActivation));
    }
    
    public void Deactivate()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.position);
        particles.Stop();
    }

    IEnumerator DeactivateLaser(float time)
    {
        yield return new WaitForSeconds(time);
        Deactivate();
    }

    IEnumerator ReloadDamage(float time)
    {
        yield return new WaitForSeconds(time);
        damageAvailable = true;
    }
}
