using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Storage cartrigeStorage;
    [SerializeField] private float timeAfterRemoveCartrige;
    [SerializeField] private Weapon weakWeapon;
    [SerializeField] private LaserGun laserGun;
    [SerializeField] private ButtonPressDuration buttonPressDuration;

    private float timeButtonClamp;
    private bool buttonClamp;
    private bool removeCartridgeAvailable;

    private void Update()
    {
        WeakAttack();
    }

    public void StartAttack()
    {
        buttonClamp = true;
        removeCartridgeAvailable = true;
    }

    public void CheckButtonClamp(float time)
    {
        if (time >= buttonPressDuration.MinTimeButtonClamp)
        {
            LaserAttack(time);
        }

        if (time != 0)
            timeButtonClamp = time;
    }

    public void StopAttack()
    {
        if (laserGun.LaserActive)
            laserGun.StartDeactivateLaserTimer();
        
        buttonClamp = false;
    }

    private void WeakAttack()
    {
        if (laserGun.LaserActive) return;
        if (timeButtonClamp == 0 || timeButtonClamp >= buttonPressDuration.MinTimeButtonClamp) return;
        if (buttonClamp) return;

        timeButtonClamp = 0;
        weakWeapon.Fire();
    }

    private void LaserAttack(float time)
    {
        if (cartrigeStorage.CurrentValue < laserGun.RequiredCartridge) return;

        if (!laserGun.LaserActive)
            laserGun.Activate();
        else
        {
            laserGun.IncreaseLaser(time);
        }

        if (removeCartridgeAvailable)
        {
            cartrigeStorage.RemoveValue(laserGun.RequiredCartridge);
            removeCartridgeAvailable = false;
            StartCoroutine(IncreaseLaserTimer(timeAfterRemoveCartrige));
        }
    }
    private IEnumerator IncreaseLaserTimer(float time)
    {
        yield return new WaitForSeconds(time);
        removeCartridgeAvailable = true;
    }
}
