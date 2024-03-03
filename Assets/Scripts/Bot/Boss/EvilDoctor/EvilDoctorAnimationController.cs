using UnityEngine;
using UnityEngine.Events;

public class EvilDoctorAnimationController : MonoBehaviour
{
    [HideInInspector] public event UnityAction OnEndAttackAnim;
    [HideInInspector] public event UnityAction OnEndTeleportAnim;

    private Animator animator;
    
    private string attackParameter = "Attack";
    private string teleportPatameter = "Teleport";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TurnOnAnimAttack()
    {
        animator.SetTrigger(attackParameter);
    }

    public void TurnOnTeleportAnimAttack()
    {
        animator.SetTrigger(teleportPatameter);
    }

    private void TurnOffAnimAttack()
    {
        OnEndAttackAnim?.Invoke();
    }

    private void TurnOffAnimTeleport()
    {
        OnEndTeleportAnim?.Invoke();
    }
}
