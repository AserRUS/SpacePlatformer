using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private Weapon weakWeapon;
    [SerializeField] private LaserGun laserGun;
    [SerializeField] private Storage cartridgeStorage;

    public void UseAttack(float timeClamp)
    {
        if (laserGun.LaserActive) return;

        if (timeClamp > buttonPressDuration.TimeLimitForButtonClamp)
        {
            if (cartridgeStorage.CurrentValue < laserGun.RequiredCartridge)
                return;

            cartridgeStorage.RemoveValue(laserGun.RequiredCartridge);
            laserGun.Activate();
        }
        else
        {
            if (cartridgeStorage.CurrentValue < weakWeapon.RequiredCartridge)
                return;

            cartridgeStorage.RemoveValue(weakWeapon.RequiredCartridge);

            weakWeapon.Fire();
        }
    }
}
