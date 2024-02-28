using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [SerializeField] private CameraFieldOfViewController viewController;
    [SerializeField] private Boss boss;

    private Destructible bossDest;
    private bool isFight;

    private void Start()
    {
        bossDest = boss.GetComponent<Destructible>();
        bossDest.DeathEvent += BossDeath;

        boss.gameObject.SetActive(false);
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
        viewController.SetBossFightFieldOfView();
        boss.gameObject.SetActive(true);
        boss.StartFight();
    }

    private void FinishFight()
    {
        viewController.SetBasicFieldOfView();
    }

    private void BossDeath()
    {
        FinishFight();
    }
}
