using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [SerializeField] private CameraFieldOfViewController viewController;
    [SerializeField] private Boss boss;
    [SerializeField] private UIState uiHealth;
    [SerializeField] private Door[] doors;
    [SerializeField] private PlayerSpawner playerSpawner;

    private Destructible bossDest;
    private Player player;
    private BoxCollider boxCollider;
    private bool isFight;

    private void Start()
    {
        bossDest = boss.GetComponent<Destructible>();
        bossDest.DeathEvent += BossDeath;
        boxCollider = GetComponent<BoxCollider>();

        boss.gameObject.SetActive(false);
        uiHealth.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        bossDest.DeathEvent -= BossDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartFight();
            SetPlayer();
        }
    }

    private void StartFight()
    {
        isFight = true;
        boxCollider.enabled = false;
        //Camera
        viewController.SmoothlySetBossFightFieldOfVision();
        //Boss
        boss.gameObject.SetActive(true);
        boss.StartFight();
        //boss.StartFight();
        //UI slider
        uiHealth.SetMaxValue(bossDest.MaxHitPoints);
        uiHealth.ValueChange(bossDest.MaxHitPoints);
        bossDest.HitPointChangeEvent += uiHealth.ValueChange;
        uiHealth.gameObject.SetActive(true);
        //Doors
        if (doors != null)
        {
            foreach (Door door in doors)
            {
                door.CloseDoor();
            }
        }
    }

    private void FinishFight()
    {
        isFight = false;
        //Camera
        viewController.SmoothlySetBasicFieldOfVision();
        //UI slider
        bossDest.HitPointChangeEvent -= uiHealth.ValueChange;
        uiHealth.gameObject.SetActive(false);
        //Doors
        if (doors != null)
        {
            foreach (Door door in doors)
            {
                door.OpenDoor();
            }
        }
    }

    private void BossDeath()
    {
        FinishFight();
    }

    private void SetPlayer()
    {
        player = playerSpawner.GetPlayer();
        player.DeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        player.DeathEvent -= OnPlayerDeath;
        SetPlayer();

        if (isFight)
        {
            FinishFight();
            boxCollider.enabled = true;
            boss.OnPlayerDeath();
            boss.gameObject.SetActive(false);
        }
    }
}
