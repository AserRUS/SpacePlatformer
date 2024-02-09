using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Weapon m_Turret;
    [SerializeField] private Movement m_Movement;

    

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Movement?.Jump();
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_Turret?.Fire();
        }
         
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            m_Movement?.SetDirection(-1);
            m_Turret?.SetDirection(-1);
        }
            
        else if (Input.GetKey(KeyCode.D))
        {
            m_Movement?.SetDirection(1);
            m_Turret?.SetDirection(1);
        }
            
        else
            m_Movement?.SetDirection(0);
        
    }    

}
