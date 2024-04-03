using UnityEngine;
using UnityEngine.Events;

public class DoorAnimationController : MonoBehaviour
{
    public event UnityAction OnClossingEnd;
    public event UnityAction OnOpenningEnd;
    public event UnityAction OnMiddlePath;

    private Animator animator;
    private string openningParameter = "Openning";
    private string clossingPatameter = "Clossing";

    private void Awake()
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

    public void OnClossingEndEvent()
    {
        OnClossingEnd?.Invoke();
    }
    public void OnOpenningEndEvent()
    {
        OnOpenningEnd?.Invoke();
    }
    public void OnMiddlePathEvent()
    {
        OnMiddlePath?.Invoke();
    }
}
