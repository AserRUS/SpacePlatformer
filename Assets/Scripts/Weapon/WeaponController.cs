using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon weakWeapon;
    [SerializeField] private LaserGun laserGun;
    [SerializeField] private Storage cartridgeStorage;

    private float timeLimitForStronAttack = 1f;

    public void UseAttack(float timeClamp)
    {
        if (timeClamp > timeLimitForStronAttack)
        {
            if (cartridgeStorage.CurrentValue < laserGun.RequiredCartridge || laserGun.LaserActive == true)
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
