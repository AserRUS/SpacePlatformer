using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Barrel : Destructible
{
    private enum BarretType
    {
        EnergyBarrel,
        HitPointBarrel,
        CartridgeBarrel
    }

    [SerializeField] private float m_LifeTime;
    [SerializeField] private BarretType m_BarrelType;
    [SerializeField] private int m_AddedValue;
    [SerializeField] private AudioClip m_FallingSound;

    private float timer;
    private AudioSource m_Audio;
    protected override void Start()
    {        
        base.Start();
        enabled = false;
        timer = m_LifeTime;

        m_Audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        enabled = true;

        m_Audio.PlayOneShot(m_FallingSound);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Death(null);
        }
    }


    protected override void Death(GameObject owner)
    {
        base.Death(owner);        

        if (owner == null) return;

        if (m_BarrelType == BarretType.HitPointBarrel)
        {
            Destructible dest = owner.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.AddHitpoints(m_AddedValue);
            }            
        }
        if (m_BarrelType == BarretType.EnergyBarrel)
        {
            Storage[] storages = owner.GetComponents<Storage>();

            if (storages != null)
            {
                foreach (Storage storage in storages)
                {
                    if (storage.StorageType == StorageType.Energy)
                    {
                        storage.AddValue(m_AddedValue);
                    }
                }
            }
        }
        if (m_BarrelType == BarretType.CartridgeBarrel)
        {
            Storage[] storages = owner.GetComponents<Storage>();

            if (storages != null)
            {
                foreach (Storage storage in storages)
                {
                    if (storage.StorageType == StorageType.Cartridge)
                    {
                        storage.AddValue(m_AddedValue);
                    }
                }
            }
        }
    }
}
