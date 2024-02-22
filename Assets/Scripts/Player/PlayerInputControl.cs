using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private ShieldController m_Shield;
    [SerializeField] private PlayerMovement m_Movement;    


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

    public void Stop(bool value)
    {
        m_Movement?.Stop(value);
    }
}
