using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private int damage;
    [SerializeField] private int mass;
    [SerializeField] private float lifeTime;
    [SerializeField] private int numberStepsToChangeTransparency;
    [SerializeField] private bool isActive;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Renderer boxRenderer;
    private float minAngle = 45;
    private float maxAngle = 135;

    public bool IsActive => isActive;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;
        boxCollider = GetComponent<BoxCollider>();

        if (isActive)
            SetActive();
        else
            SetNotActive();
    }

    private void FixedUpdate()
    {
        UseGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        Destructible destructible = collision.gameObject.GetComponent<Destructible>();
        
        if (destructible)
        {
            Vector3 direction = transform.position - collision.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < maxAngle && angle > minAngle && rb.velocity.y < 0)
            {
                destructible.RemoveHitpoints(damage, destructible.gameObject);
                Destroy(gameObject);
            }
        }

        if ((groundMask & (1 << collision.gameObject.layer)) != 0)
        {
            StartCoroutine(LifeTimer(lifeTime));
        }
    }

    private void UseGravity()
    {
        if (!IsActive) return;
        
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    private IEnumerator LifeTimer(float time)
    {
        if (!isActive) yield return null;

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void StartTransitionToActive(float timeWarningAttack)
    {
        boxRenderer = GetComponentInChildren<Renderer>();
        Color color = boxRenderer.material.color;
        color.a = 0;
        boxRenderer.material.color = color;

        float step = 1f / (float) numberStepsToChangeTransparency;
        float timeStep = timeWarningAttack / numberStepsToChangeTransparency;
        StartCoroutine(ChangeTransparency(step, timeStep));
    }

    public void SetActive()
    {
        isActive = true;
        rb.isKinematic = false;
        boxCollider.enabled = true;
    }

    public void SetNotActive()
    {
        isActive = false;
        rb.isKinematic = true;
        boxCollider.enabled = false;
    }

    private IEnumerator ChangeTransparency(float step, float time)
    {
        yield return new WaitForSeconds(time);

        Color color = boxRenderer.material.color;
        color.a = color.a + step;
        boxRenderer.material.color = color;

        if (boxRenderer.material.color.a < 1)
        {
            StartCoroutine(ChangeTransparency(step, time));
        }
        else
        {
            SetActive();
        }
    }
}
