using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [SerializeField] private CameraFieldOfViewController viewController;
    [SerializeField] private Boss boss;
    [SerializeField] private UIState uiHealth;
    [SerializeField] private Door[] doors;

    private Destructible bossDest;

    private void Start()
    {
        bossDest = boss.GetComponent<Destructible>();
        bossDest.DeathEvent += BossDeath;

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
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;

            StartFight();
        }
    }

    private void StartFight()
    {
        //Camera
        viewController.SmoothlySetBossFightFieldOfVision();
        //Boss
        boss.gameObject.SetActive(true);
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
}
