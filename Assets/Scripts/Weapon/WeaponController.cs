using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon weakWeapon;
    [SerializeField] private Weapon strongWeapon;

    private float timeLimitForStrongShield = 1f;

    public void UseAttack(float timeClamp)
    {
        if (timeClamp > timeLimitForStrongShield)
        {
            strongWeapon.Fire();
        }
        else
        {
            weakWeapon.Fire();
        }
    }
}
