using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerSpawner : EntitySpawner
{
    [SerializeField] InputControl m_InputControl;
    [SerializeField] private CameraController m_CameraController;
    [SerializeField] private float RespawnTime;
    [SerializeField] private UIState m_UIHealth;
    [SerializeField] private UIState m_UIEnergy;
    [SerializeField] private UIState m_UICartridge;

    private Player player;


    private void Awake()
    {
        EntitySpawn();
    }
    protected override void EntitySpawn()
    {
        GameObject entity = Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)], m_SpawnPosition.position, Quaternion.identity);

        player = entity.GetComponent<Player>();
        if (player != null) 
        {            
            m_CameraController.SetCameraFollowTarget(player.transform);
            player.DeathEvent += Respawn;
            m_UIHealth.SetMaxValue(player.MaxHitPoints);
            player.HitPointChangeEvent += m_UIHealth.ValueChange;
        }        

        Storage[] storages = entity.GetComponents<Storage>();
        if (storages != null)
        {
            foreach (Storage storage in storages)
            {
                if (storage.StorageType == StorageType.Energy)
                {
                    m_UIEnergy.SetMaxValue(storage.MaxValue);
                    storage.StorageChangeEvent += m_UIEnergy.ValueChange;
                }

                if (storage.StorageType == StorageType.Cartridge)
                {
                    m_UICartridge.SetMaxValue(storage.MaxValue);
                    storage.StorageChangeEvent += m_UICartridge.ValueChange;
                }
            }
        }

        PlayerInputControl playerInputControl = entity.GetComponent<PlayerInputControl>();
        if (playerInputControl != null)
        {
            m_InputControl.SetPlayerInputControl(playerInputControl);
        }

    }

    private void Respawn()
    {
        player.HitPointChangeEvent -= m_UIHealth.ValueChange;
        player.DeathEvent -= Respawn;
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(RespawnTime);
        EntitySpawn();
    }

    public Player GetPlayer()
    {
        return player;
    }
    
}
