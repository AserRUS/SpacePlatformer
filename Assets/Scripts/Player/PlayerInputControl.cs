using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private Movement m_Movement;    

       

    public void Jump()
    {
        m_Movement?.Jump();
    }

    public void Fire()
    {
        m_Weapon?.Fire();
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
