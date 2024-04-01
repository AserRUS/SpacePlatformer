using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    private Animator animator;

    private string openningParameter = "Openning";
    private string clossingPatameter = "Clossing";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger(openningParameter);
    }

    public void CloseDoor()
    {
        animator.SetTrigger(clossingPatameter);
    }
}
