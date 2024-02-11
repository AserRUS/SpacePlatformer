using UnityEngine;

public abstract class EntitySpawner : MonoBehaviour
{
    
    [SerializeField] protected GameObject[] m_EntityPrefabs;
    [SerializeField] protected Transform m_SpawnPosition;

    public void SetSpawnPosition(Transform spawnPosition)
    {
        m_SpawnPosition = spawnPosition;
    }
    protected abstract void EntitySpawn();
    
}
