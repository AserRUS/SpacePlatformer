using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
     
    [SerializeField] private Projectile m_ProjectilePrefab;
    [SerializeField] private float m_RateOfFire;    
    [SerializeField] private Transform m_Gunpoint;   
    [SerializeField] private ParticleSystem m_Flash;
    [SerializeField] private AudioClip m_ShotSound;

    private int direction = 1;
    private AudioSource m_Audio; 
    private float m_RefireTimer;
    
    private void Start()
    {        
        m_Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_RefireTimer > 0)
            m_RefireTimer -= Time.deltaTime;   
    }   
    

    public void Fire()
    {
        if (m_RefireTimer > 0) return;     
        Projectile projectile = Instantiate(m_ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = m_Gunpoint.position;
        projectile.transform.up = new Vector2(direction, 0);
        projectile.SetOwner(transform.root.gameObject);
        
        //m_Flash.Play();
        //m_Audio.PlayOneShot(m_ShotSound);
        m_RefireTimer = m_RateOfFire;
    }
    public void SetDirection(int dir)
    {
        direction = dir;
    }
}

