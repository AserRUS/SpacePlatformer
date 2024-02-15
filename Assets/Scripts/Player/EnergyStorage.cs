using UnityEngine;
using UnityEngine.Events;

public class EnergyStorage : MonoBehaviour
{
    public event UnityAction<int> EnergyChangeEvent;
    public int MaxEnergy => m_MaxEnergy;
    public int CurrentEnergy => currentEnergy;


    [SerializeField] private int m_MaxEnergy;

    private int currentEnergy;

    private void Start()
    {
        currentEnergy = 0;
        EnergyChangeEvent?.Invoke(currentEnergy);
    }
    public void RemoveEnergy(int value)
    {
        currentEnergy -= value;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }

        EnergyChangeEvent?.Invoke(currentEnergy);        
    }
    public void AddEnergy(int value)
    {
        currentEnergy += value;

        if (currentEnergy > m_MaxEnergy)
        {
            currentEnergy = m_MaxEnergy;
        }
        EnergyChangeEvent?.Invoke(currentEnergy);
    }
}
