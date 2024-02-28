using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFieldOfViewController : MonoBehaviour
{
    [SerializeField] private float basicFieldOfView;
    [SerializeField] private float bossFightFieldOfView;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void SetBasicFieldOfView()
    {
        camera.fieldOfView = basicFieldOfView;
    }

    public void SetBossFightFieldOfView()
    {
        camera.fieldOfView = bossFightFieldOfView;
    }
}
