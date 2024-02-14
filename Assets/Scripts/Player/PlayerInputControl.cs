using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private PlayerMovement m_Movement;    

       

    public void Jump()
    {
        m_Movement?.Jump();
    }

    public void Fire()
    {
        m_Weapon?.Fire();
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
