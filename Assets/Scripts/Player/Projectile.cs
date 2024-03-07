using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Velosity;
    [SerializeField] private GameObject m_ImpactEffect;
    [SerializeField] private float m_LifeTime;
    [SerializeField] private LayerMask m_LayerMask;

    private GameObject owner;
    private const float RayAdvance = 1.1f;
    private Vector3 step;
    private bool isHit;
    private RaycastHit raycastHit;

    protected virtual void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f, m_LayerMask);
        if (colliders.Length != 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Destructible dest = colliders[i].transform.root.GetComponent<Destructible>();
                if (dest != null)
                {
                    dest.RemoveHitpoints(m_Damage, owner);
                    
                }
            }
            
            Destruction();
        }
        else
        {
            StartCoroutine(LifeTime());
        }
       
    }
    
    private void Update()
    {
        Move();
        Chek();
    }

    
    private void Move()
    {
        step = transform.up * m_Velosity * Time.deltaTime;

        transform.position += step;
    }
    private void Chek()
    {
        if (isHit == true) return;

        if (Physics.Raycast(transform.position, transform.up, out raycastHit, m_Velosity * Time.deltaTime * RayAdvance, m_LayerMask))
        {
            transform.position = raycastHit.point;
            Destructible dest = raycastHit.transform.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.RemoveHitpoints(m_Damage, owner);
            }
            Destruction();

            isHit = true;
        }

    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(m_LifeTime);
        Destruction();        
    }

    private void Destruction()
    {
        Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
    
}

