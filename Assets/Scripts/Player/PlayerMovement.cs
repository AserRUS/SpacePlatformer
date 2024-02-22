using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_StepCheckDistance = 1.3f;
    [SerializeField] private Vector3 m_StepCheckOffset;
    [SerializeField] private float m_GroundCheckDistance = 1.3f;
    [SerializeField] private LayerMask m_LayerMask;

    [Header("Jump")]
    [SerializeField] private Vector2 m_JumpForce;
    [SerializeField] private float m_StunTime;


    private Rigidbody rb;

    private bool isGround;
    private bool isStun;
    private bool isStop;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        Movement();
        Resistance();
    }

    private void CheckGround()
    {
        isGround = Physics.Raycast(transform.position, -transform.up, m_GroundCheckDistance, m_LayerMask);

        Debug.DrawRay(transform.position, -Vector2.up * m_GroundCheckDistance, Color.yellow);
    }

    private bool CheckJumpOpportunity()
    {
        return Physics.Raycast(transform.position, transform.up, m_GroundCheckDistance, m_LayerMask);
    }

    private bool CheckMoveOpportunity()
    {
        Debug.DrawRay(transform.position + m_StepCheckOffset, transform.right * m_StepCheckDistance, Color.yellow);
        return Physics.Raycast(transform.position, transform.right, m_StepCheckDistance, m_LayerMask);
        
    }

    private void Movement()
    {
        if (CheckMoveOpportunity() == true) return;
        if (isStun == true) return;
        if (isStop == true) return;

        rb.AddForce(transform.right * m_GroundSpeed * Time.fixedDeltaTime);
    }

         
    public void Jump()
    {
        if (isGround == false) return;
        if (isStun == true) return;
        if (CheckJumpOpportunity() == true) return;
                    
        
        rb.AddForce( new Vector2(m_JumpForce.x, m_JumpForce.y), ForceMode.Impulse);        
    }

    private void Resistance()
    {
        if (isStun == true) return;
        rb.AddForce(new Vector3(-rb.velocity.x * (m_GroundSpeed / m_MaxSpeed), 0, 0) * Time.fixedDeltaTime);
    }
    
    public void Stun()
    {        
        StartCoroutine(StunTimer());
    }

    public void Stop(bool value)
    {
        isStop = value;
    }

    private IEnumerator StunTimer()
    {
        isStun = true;
        yield return new WaitForSeconds(m_StunTime);
        isStun = false;
    }
}
