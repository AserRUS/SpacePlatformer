using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public event UnityAction OnJumpEvent;
    public event UnityAction OnRotationEvent;

    public bool IsRotation => isRotation;
    public Vector3 Velocity => rb.velocity;
    public float MaxSpeed => m_MaxSpeed;
    public float DistanceToGround => m_DistanceToGround;
    public bool IsGround => isGround;

    [Header("Movement")]
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RayDistance;
    [SerializeField] private Vector3 m_RayOffset;
    [SerializeField] private LayerMask m_LayerMask;

    [Header("Jump")]
    [SerializeField] private Vector2 m_JumpForce;
    [SerializeField] private int m_MaxJumpCount;
    [SerializeField] private float m_StunTime;

    private float m_DistanceToGround;
    
    private Rigidbody rb;

    private int direction = 1;
    private int jumpCount;
    private bool isGround;
    private bool isMove;
    private bool isRotation;
    private bool isStun;
    private Vector3 targetRotation;
    private RaycastHit hit;
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
        m_DistanceToGround = 100;
        float dist;
        if (Physics.Raycast(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -transform.up, out hit, 100, m_LayerMask) == true)
        {
            dist = Vector3.Distance(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), hit.point);
            if (dist < m_DistanceToGround)
                m_DistanceToGround = dist;
        }
        if (Physics.Raycast(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -transform.up, out hit, 100, m_LayerMask) == true)
        {
            dist = Vector3.Distance(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), hit.point);
            if (dist < m_DistanceToGround)
                m_DistanceToGround = dist;
        }

        if (m_DistanceToGround <= m_RayDistance)
        {
            if (isGround == false)
            {
                jumpCount = m_MaxJumpCount;
            }
            isGround = true;
        }
        else
        {
            isGround = false;
        }


        Debug.DrawRay(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -Vector2.up * m_RayDistance, Color.yellow);
        Debug.DrawRay(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -Vector2.up * m_RayDistance, Color.yellow);
    }

    private bool CheckJumpOpportunity()
    {
        return Physics.Raycast(transform.position, transform.up, m_RayDistance, m_LayerMask);
    }


    private void Movement()
    {
        if (isStun == true) return;
        if (isMove == false) return;
        
        if (isGround != false)
            rb.AddForce(direction * Vector3.right * m_GroundSpeed);
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

        OnJumpEvent?.Invoke();
    }

    private void Resistance()
    {
        if (rb.velocity.x * direction >= m_MaxSpeed && isGround)
        {
            rb.velocity = new Vector3(direction * m_MaxSpeed, rb.velocity.y, rb.velocity.z);
        }  
    }

    public void RotateLeft()
    {
        if (direction == -1) return;
        targetRotation = new Vector3(0, 180, 0);
        isRotation = true;
        direction = -1;

        OnRotationEvent?.Invoke();
    }
    public void RotateRight()
    {
        if (direction == 1) return;
        targetRotation = new Vector3(0, 0, 0);
        isRotation = true;
        direction = 1;

        OnRotationEvent?.Invoke();
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
