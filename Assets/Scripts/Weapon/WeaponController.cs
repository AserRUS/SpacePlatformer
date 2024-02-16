using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon weakWeapon;
    [SerializeField] private Weapon strongWeapon;
    [SerializeField] private Storage cartridgeStorage;

    private float timeLimitForStrongShield = 1f;

    public void UseAttack(float timeClamp)
    {
        if (timeClamp > timeLimitForStrongShield)
        {
            if (cartridgeStorage.CurrentValue < strongWeapon.RequiredEnergy)
                return;

            cartridgeStorage.RemoveValue(strongWeapon.RequiredEnergy);

            strongWeapon.Fire();
        }
        else
        {
            if (cartridgeStorage.CurrentValue < weakWeapon.RequiredEnergy)
                return;

            cartridgeStorage.RemoveValue(weakWeapon.RequiredEnergy);

            weakWeapon.Fire();
        }
    }
}
