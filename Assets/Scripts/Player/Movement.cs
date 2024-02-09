using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_AirSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RayDistance;
    [SerializeField] private float m_FrictionForce;

    [Header("Jump")]
    [SerializeField] private float m_JumpForce;
    [SerializeField] private int m_MaxJumpCount;



    private Rigidbody2D rb;
    private float direction;
    private bool isGround;
    private int jumpCount;
    private RaycastHit2D hit;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = m_MaxJumpCount;
    }
    private void Update()
    {
        CheckGround();   
        
    }
    private void FixedUpdate()
    {  
        Move();
        Friction();
    }

    private void CheckGround()
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, m_RayDistance);
        if (isGround == false && hit != false)
        {
            jumpCount = m_MaxJumpCount;
        }
        isGround = hit;

        Debug.DrawRay(transform.position, -Vector2.up * m_RayDistance, Color.yellow);
    }

    
    private void Move()
    {
        if (Mathf.Abs(rb.velocity.x) > m_MaxSpeed) return;

        if (isGround == false)
            rb.AddForce(new Vector2(direction * m_AirSpeed, 0));
        else 
            rb.AddForce(new Vector2(direction * m_GroundSpeed, 0));
    }
    public void Jump()
    {        
        if (jumpCount == 0) return;

        if (isGround == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
        rb.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);

        jumpCount--;
    }

    private void Friction()
    {
        if (isGround)
        { 
            rb.AddForce(-Vector2.up * m_FrictionForce);
        }

    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }
}
