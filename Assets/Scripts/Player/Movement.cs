using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_AirSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RayDistance;
    [SerializeField] private LayerMask m_LayerMask;

    [Header("Jump")]
    [SerializeField] private float m_JumpForce;
    [SerializeField] private int m_MaxJumpCount;



    private Rigidbody rb;
    private float direction;
    private bool isGround;
    private int jumpCount;
    private bool isHit;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpCount = m_MaxJumpCount;
    }
    private void Update()
    {
        CheckGround();   
        
    }
    private void FixedUpdate()
    {  
        Move();
        Resistance();
    }

    private void CheckGround()
    {
        isHit = Physics.Raycast(transform.position, -transform.up, m_RayDistance, m_LayerMask);
        if (isGround == false && isHit != false)
        {
            jumpCount = m_MaxJumpCount;
        }
        isGround = isHit;

        Debug.DrawRay(transform.position, -Vector2.up * m_RayDistance, Color.yellow);
    }

    
    private void Move()
    {       

        if (isGround == false)
            rb.AddForce(new Vector2(direction , 0) * m_AirSpeed * Time.fixedDeltaTime);
        else 
            rb.AddForce(new Vector2(direction , 0) * m_GroundSpeed * Time.fixedDeltaTime);
    }
    public void Jump()
    {        
        if (jumpCount == 0) return;

        if (isGround == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
        rb.AddForce(new Vector2(0, m_JumpForce), ForceMode.Impulse);

        jumpCount--;
    }

    private void Resistance()
    {
        if (isGround)
        {
            rb.AddForce(-rb.velocity * (m_GroundSpeed / m_MaxSpeed) * Time.fixedDeltaTime);
        }
            
        else
        {
            rb.AddForce(new Vector2(1, 0) * -rb.velocity * (m_AirSpeed / m_MaxSpeed) * Time.fixedDeltaTime);
        }

    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }
}
