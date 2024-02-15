using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsRotation => isRotation;

    [Header("Movement")]
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_AirSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RayDistance;
    [SerializeField] private LayerMask m_LayerMask;

    [Header("Jump")]
    [SerializeField] private Vector2 m_JumpForce;
    [SerializeField] private int m_MaxJumpCount;
    [SerializeField] private float m_StunTime;


    private Rigidbody rb;

    private int direction = 1;
    private int jumpCount;
    private bool isGround;
    private bool isMove;
    private bool isRotation;
    private bool isStun;
    private Vector3 targetRotation;

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
        Movement();
        Resistance();
        Rotation();
    }

    private void CheckGround()
    {
        bool isHit = Physics.Raycast(transform.position, -transform.up, m_RayDistance, m_LayerMask);
        if (isGround == false && isHit != false)
        {
            jumpCount = m_MaxJumpCount;
        }
        isGround = isHit;

        Debug.DrawRay(transform.position, -Vector2.up * m_RayDistance, Color.yellow);
    }

    private bool CheckJumpOpportunity()
    {
        return Physics.Raycast(transform.position, transform.up, m_RayDistance, m_LayerMask);
    }


    private void Movement()
    {
        if (isMove == false) return;
        if (isGround == false)
            rb.AddForce(direction * Vector3.right * m_AirSpeed * Time.fixedDeltaTime);
        else 
            rb.AddForce(direction * Vector3.right * m_GroundSpeed * Time.fixedDeltaTime);
    }

    private void Rotation()
    {
        if (isRotation == false) return;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = Vector3.MoveTowards(rotation, targetRotation, m_RotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.Euler(rotation));

        if (transform.rotation.eulerAngles == targetRotation)
        {
            isRotation = false;
        }
    }

    public void Jump()
    {
        if (isStun == true) return;
        if (jumpCount == 0) return;
        if (CheckJumpOpportunity() == true) return;
        
        rb.velocity = Vector2.zero;       
        
        rb.AddForce( new Vector2(m_JumpForce.x * direction, m_JumpForce.y), ForceMode.Impulse);

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

    public void RotateLeft()
    {
        if (direction == -1) return;
        targetRotation = new Vector3(0, 180, 0);
        isRotation = true;
        direction = -1;
    }
    public void RotateRight()
    {
        if (direction == 1) return;
        targetRotation = new Vector3(0, 0, 0);
        isRotation = true;
        direction = 1;
    }

    public void Move(bool isMove)
    {
        this.isMove = isMove;
    }
    public void Stun()
    {        
        StartCoroutine(StunTimer());
    }
    private IEnumerator StunTimer()
    {
        isStun = true;
        yield return new WaitForSeconds(m_StunTime);
        isStun = false;
    }
}