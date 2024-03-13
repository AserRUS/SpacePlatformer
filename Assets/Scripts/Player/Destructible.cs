using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public event UnityAction DeathEvent;
    public event UnityAction<int> HitPointChangeEvent;
    public int MaxHitPoints => m_MaxHitPoints;


    [SerializeField] private int m_MaxHitPoints;
    [SerializeField] private GameObject m_DeathEffect;

    private int currentHitPoints;

    protected virtual void Start()
    {
        currentHitPoints = m_MaxHitPoints;
        HitPointChangeEvent?.Invoke(currentHitPoints);
    }
    public virtual void RemoveHitpoints(int value, GameObject owner)
    {
        currentHitPoints -= value;

        if (currentHitPoints < 0)
        {
            currentHitPoints = 0;
        }

        HitPointChangeEvent?.Invoke(currentHitPoints);

        if (currentHitPoints == 0)
        {            
            DeathEvent?.Invoke();
            Death(owner);
        }
    }
    public void AddHitpoints(int value)
    {
        currentHitPoints += value;

        if (currentHitPoints > m_MaxHitPoints)
        {
            currentHitPoints = m_MaxHitPoints;            
        }
        HitPointChangeEvent?.Invoke(currentHitPoints);
    }

    protected virtual void Death(GameObject owner)
    {
        Instantiate (m_DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
