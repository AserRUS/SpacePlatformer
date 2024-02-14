using System.Collections;
using UnityEngine;

public class BotWeapon : MonoBehaviour
{
    [SerializeField] private BotAnimatorController animController;

    [Header("Ranged attack")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int maxNumberCartridges;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float timeRecharge;

    [Header("Melee attack")]
    [SerializeField] private int meleeDamage;

    private int numberCartridges;
    private bool shootAvailable;
    private bool dirRight;

    private void Start()
    {
        shootAvailable = true;
        numberCartridges = maxNumberCartridges;

        animController.OnEndAttackAnim += Shoot;
    }

    private void OnDestroy()
    {
        animController.OnEndAttackAnim -= Shoot;
    }

    public void TryAttack(bool dirRight)
    {
        if (numberCartridges <= 0) return;
        if (!shootAvailable)  return;

        this.dirRight = dirRight;
        numberCartridges -= 1;
        shootAvailable = false;

        animController.TurnOnAnimAttack();
    }
    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        shootAvailable = true;

        if (numberCartridges <= 0)
            numberCartridges = maxNumberCartridges;
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = gameObject.transform.position;

        if (dirRight)
            projectile.transform.up = new Vector2(1, 0);
        else
            projectile.transform.up = new Vector2(-1, 0);

        projectile.SetOwner(transform.root.gameObject);


        if (numberCartridges <= 0)
            StartCoroutine(Reload(timeRecharge));
        else
            StartCoroutine(Reload(timeBetweenShots));
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
