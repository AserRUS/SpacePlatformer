using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorModel;
    [SerializeField] private bool openInStart;

    private DoorAnimationController animationController;

    private void Start()
    {
        animationController = GetComponentInChildren<DoorAnimationController>();

        if (!openInStart)
            CloseDoor();
    }

    public void OpenDoor()
    {
        animationController.OpenDoor();
    }

    public void CloseDoor()
    {
        animationController.CloseDoor();
    }
}
