using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private WeaponController m_Weapon;
    [SerializeField] private ShieldController m_Shield;
    [SerializeField] private PlayerMovement m_Movement;    

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
