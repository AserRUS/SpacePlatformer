using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_Velosity;
    [SerializeField] private GameObject m_ImpactEffect;
    [SerializeField] private float m_LifeTime;

       
    protected Rigidbody2D m_Rigidbody;
    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(LifeTime());
    }
    
    private void FixedUpdate()
    {
        if (m_Rigidbody != null)
            m_Rigidbody.velocity = transform.up * m_Velosity;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {          
        Destruction();
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(m_LifeTime);
        Destruction();        
    }

    public virtual void Destruction()
    {
        Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}

