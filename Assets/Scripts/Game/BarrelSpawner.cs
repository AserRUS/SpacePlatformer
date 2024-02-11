using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : EntitySpawner
{
    [SerializeField] private float m_SpawnTime;
    [SerializeField] private float m_SpawnHeight;
    [SerializeField] private float m_SpawnRadius;
    [SerializeField] private PlayerSpawner m_PlayerSpawner;

    private float timer;

    private void Start()
    {
        timer = m_SpawnTime;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            EntitySpawn();
            timer = m_SpawnTime;
        }
    }
    protected override void EntitySpawn()
    {
        Player player = m_PlayerSpawner.GetPlayer();
        if (player == null) return;
        m_SpawnPosition.position = new Vector3 (Random.insideUnitCircle.x, 0 ,0) * m_SpawnRadius + player.transform.position + transform.up * m_SpawnHeight;

        GameObject entity = Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)], m_SpawnPosition.position, Quaternion.identity);
    }
}
