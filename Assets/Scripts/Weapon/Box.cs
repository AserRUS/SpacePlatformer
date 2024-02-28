using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private int damage;
    [SerializeField] private int mass;
    [SerializeField] private float lifeTime;
    
    private Rigidbody rb;
    private float minAngle = 45;
    private float maxAngle = 135;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    private void OnCollisionEnter(Collision collision)
    {
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

    private IEnumerator LifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
