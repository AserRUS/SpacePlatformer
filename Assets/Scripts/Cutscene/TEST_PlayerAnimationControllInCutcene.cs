using UnityEngine;

public class TEST_PlayerAnimationControllInCutcene : MonoBehaviour
{
    private Animator animator;

    private string isGroundParametr = "IsGround";

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isGroundParametr, true);
    }


}
