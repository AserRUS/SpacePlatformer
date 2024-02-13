using UnityEngine;
using UnityEngine.Events;

public class BotAnimatorController : MonoBehaviour
{
    [HideInInspector] public event UnityAction OnEndAttackAnim;

    private Animator animator;
    private Rigidbody rb;

    private string speedParameter = "Speed";
    private string attackParameter = "Attack";

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        SetSpeed();
    }

    private void SetSpeed()
    {
        animator.SetFloat(speedParameter, Mathf.Abs(rb.velocity.x));
    }

    public void TurnOnAnimAttack()
    {
        animator.SetTrigger(attackParameter);
    }

    private void TurnOffAnimAttack()
    {
        OnEndAttackAnim?.Invoke();
    }
}
