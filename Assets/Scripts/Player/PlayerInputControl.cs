using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Movement;

    [SerializeField] private ShieldController m_Shield;
    [SerializeField] private WeaponController m_Weapon;

    public void Jump()
    {
        m_Movement?.Jump();
    }

    public void IncreaseShield()
    {
        m_Shield.IncreaseShield();
    }

    public void StopShieldIncrease()
    {
        m_Shield.StopShieldIncrease();
    }

    public void StartAttack()
    {
        m_Weapon.StartAttack();
    }

    public void CheckAttackButtonClamp(float time)
    {
        m_Weapon.CheckButtonClamp(time);
    }

    public void StopAttack()
    {
        m_Weapon.StopAttack();
    }

    public void RotateLeft()
    {
        m_Movement?.RotateLeft();
    }
    public void RotateRight()
    {
        m_Movement?.RotateRight();
    }

    public void Move(bool isMove)
    {
        m_Movement?.Move(isMove);
    }
}
