using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private Movement m_Movement;
    [SerializeField] private ShieldController m_Shield;

    public void Jump()
    {
        m_Movement?.Jump();
    }

    public void Fire()
    {
        m_Weapon?.Fire();
    }

    public void UseShield(float timeClamp)
    {
        m_Shield?.UseShield(timeClamp);
    }

    public void SetMovementDirection(int direction)
    {
        m_Movement?.SetDirection(direction);
    }
    public void SetWeaponDirection(int direction)
    {
        m_Weapon?.SetDirection(direction);
    }
}
