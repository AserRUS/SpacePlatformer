using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_PlayerMovement;
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private Animator m_PlayerAnimator;
    [SerializeField] private LaserGun m_LaserGun;
    private void Start()
    {
        m_Weapon.ShotEvent += ShotAnimation;
        m_LaserGun.LaserActivateEvent += LaserAnimation;
        m_LaserGun.LaserDeactivateEvent += LaserAnimationStop;
    }
    private void OnDestroy()
    {
        m_Weapon.ShotEvent -= ShotAnimation;
        m_LaserGun.LaserActivateEvent -= LaserAnimation;
        m_LaserGun.LaserDeactivateEvent -= LaserAnimationStop;
    }

    private void Update()
    {
        m_PlayerAnimator.SetFloat("Velocity", Mathf.Abs(m_PlayerMovement.Velocity.x) /  m_PlayerMovement.MaxSpeed);
        m_PlayerAnimator.SetFloat("DistanceToGround", m_PlayerMovement.DistanceToGround);
        m_PlayerAnimator.SetBool("IsGround", m_PlayerMovement.IsGround);
    }

    private void ShotAnimation()
    {
        m_PlayerAnimator.SetTrigger("isShoot");
    }
    private void LaserAnimation()
    {
        m_PlayerAnimator.SetTrigger("isLaserStart");
    }
    private void LaserAnimationStop()
    {
        m_PlayerAnimator.SetTrigger("isLaserStop");
    }
}
