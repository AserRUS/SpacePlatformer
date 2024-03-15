using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrels : Destructible
{
    [SerializeField] private float m_Radius;
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_ImactForce;

    
    protected override void Death(GameObject owner)
    {
        base.Death(owner);

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject) continue;

            Destructible dest = colliders[i].transform.GetComponent<Destructible>();
            if (dest == null) continue;
            dest.RemoveHitpoints(m_Damage, gameObject);

           
            Rigidbody rb = colliders[i].transform.GetComponent<Rigidbody>();
            if (rb == null) continue;
            rb.velocity = Vector3.zero;
            rb.AddForce((Vector2)(rb.transform.position - transform.position).normalized * m_ImactForce, ForceMode.Impulse);
           
        }


    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
#endif
}
