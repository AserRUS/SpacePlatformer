using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [SerializeField] private CameraFieldOfViewController viewController;
    [SerializeField] private Boss boss;
    [SerializeField] private UIState uiHealth;

    private Destructible bossDest;
    private bool isFight;

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
        viewController.SetBossFightFieldOfView();
        //Boss
        boss.gameObject.SetActive(true);
        boss.StartFight();
        //UI slider
        uiHealth.SetMaxValue(bossDest.MaxHitPoints);
        uiHealth.ValueChange(bossDest.MaxHitPoints);
        bossDest.HitPointChangeEvent += uiHealth.ValueChange;
        uiHealth.gameObject.SetActive(true);
    }

    private void FinishFight()
    {
        //Camera
        viewController.SetBasicFieldOfView();
        //UI slider
        bossDest.HitPointChangeEvent -= uiHealth.ValueChange;
        uiHealth.gameObject.SetActive(false);
    }

    private void BossDeath()
    {
        FinishFight();
    }
}
