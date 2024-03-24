using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private WeaponController m_Weapon;
    [SerializeField] private ShieldController m_Shield;
    [SerializeField] private PlayerMovement m_Movement;

    [SerializeField] private New_ShieldController m_Shield_New;
    [SerializeField] private New_WeaponController m_Weapon_New;

    public void Jump()
    {
        m_Movement?.Jump();
    }

    public void UseAttack(float timeClamp)
    {
        m_Weapon?.UseAttack(timeClamp);
    }

    public void UseShield(float timeClamp)
    {
        m_Shield?.UseShield(timeClamp);
    }

    public void IncreaseShield()
    {
        m_Shield_New.IncreaseShield();
    }

    public void StopShieldIncrease()
    {
        m_Shield_New.StopShieldIncrease();
    }

    public void StartAttack()
    {
        m_Weapon_New.StartAttack();
    }

    public void CheckButtonClamp(float time)
    {
        m_Weapon_New.CheckButtonClamp(time);
    }

    public void StopAttack()
    {
        m_Weapon_New.StopAttack();
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
