using System.Collections;
using UnityEngine;

public class BotWeapon : MonoBehaviour
{
    [Header("Ranged attack")]
    // prefab projectile
    [SerializeField] private float cartridgeDamage;
    [SerializeField] private int maxNumberCartridges;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float timeRecharge;

    [Header("Melee attack")]
    [SerializeField] private float meleeDamage;

    private int numberCartridges;
    private bool shootAvailable;


    private void Start()
    {
        shootAvailable = true;
        numberCartridges = maxNumberCartridges;
    }

    public void Attack()
    {
        if (numberCartridges <= 0) return;
        if (!shootAvailable)  return;

        CreateProjectile();
        numberCartridges -= 1;
        shootAvailable = false;

        if (numberCartridges <= 0)
            StartCoroutine(Shoot(timeRecharge));
        else
            StartCoroutine(Shoot(timeBetweenShots));
    }
    IEnumerator Shoot(float time)
    {
        yield return new WaitForSeconds(time);
        shootAvailable = true;

        if (numberCartridges <= 0)
            numberCartridges = maxNumberCartridges;
    }

    private void CreateProjectile()
    {
        Debug.Log("PiuPiu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Нанести урон милишкой по игроку
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject.transform.root.gameObject);
    }
}
