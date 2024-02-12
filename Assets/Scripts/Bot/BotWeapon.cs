using System.Collections;
using UnityEngine;

public class BotWeapon : MonoBehaviour
{
    [Header("Ranged attack")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int maxNumberCartridges;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float timeRecharge;

    [Header("Melee attack")]
    [SerializeField] private int meleeDamage;

    private int numberCartridges;
    private bool shootAvailable;


    private void Start()
    {
        shootAvailable = true;
        numberCartridges = maxNumberCartridges;
    }

    public void Attack(bool dirRight)
    {
        if (numberCartridges <= 0) return;
        if (!shootAvailable)  return;

        CreateProjectile(dirRight);
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

    private void CreateProjectile(bool dirRight)
    {
        Projectile projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = gameObject.transform.position;

        if (dirRight)
            projectile.transform.up = new Vector2(1, 0);
        else
            projectile.transform.up = new Vector2(-1, 0);

        projectile.SetOwner(transform.root.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destructible dest = other.GetComponent<Destructible>();

            if (dest)
            {
                dest.RemoveHitpoints(meleeDamage, gameObject);
            }

            Destroy(gameObject.transform.root.gameObject);
        }
    }
}
