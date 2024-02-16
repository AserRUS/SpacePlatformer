using UnityEngine;
using UnityEngine.Events;

public enum StorageType
{
    Energy,
    Cartridge
}

public class Storage : MonoBehaviour
{
    public event UnityAction<int> StorageChangeEvent;
    public int MaxValue => m_MaxValue;
    public int CurrentValue => m_CurrentValue;
    public StorageType StorageType => m_StorageType;

    [SerializeField] private StorageType m_StorageType;
    [SerializeField] private int m_InitialValue;
    [SerializeField] private int m_MaxValue;

    private int m_CurrentValue;

    private void Start()
    {
        m_CurrentValue = m_InitialValue;
        StorageChangeEvent?.Invoke(m_CurrentValue);
    }
    public void RemoveValue(int value)
    {
        m_CurrentValue -= value;

        if (m_CurrentValue < 0)
        {
            m_CurrentValue = 0;
        }

        StorageChangeEvent?.Invoke(m_CurrentValue);        
    }
    public void AddValue(int value)
    {
        m_CurrentValue += value;

        if (m_CurrentValue > m_MaxValue)
        {
            m_CurrentValue = m_MaxValue;
        }
        StorageChangeEvent?.Invoke(m_CurrentValue);
    }
}
