using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorModel;
    [SerializeField] private bool openInStart;

    private void Start()
    {
        if (openInStart)
            OpenDoor();
        else
            CloseDoor();
    }

    public void OpenDoor()
    {
        doorModel.SetActive(false);
    }

    public void CloseDoor()
    {
        doorModel.SetActive(true);
    }
}
