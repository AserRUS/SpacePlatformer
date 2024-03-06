using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFieldOfViewController : MonoBehaviour
{
    [Range(0, 160)]
    [SerializeField] private float basicFieldOfView = 36.7f;
    [Range(0, 160)]
    [SerializeField] private float bossFightFieldOfView = 55.7f;
    [SerializeField] private float speed;

    private new Camera camera;
    private bool isIncreases;
    private bool isDecreases;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ChangeFieldOfView();
    }

    private void ChangeFieldOfView()
    {
        if (isIncreases)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, bossFightFieldOfView, speed * Time.deltaTime);

            if (camera.fieldOfView >= bossFightFieldOfView)
                isIncreases = false;
        }

        if (isDecreases)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, basicFieldOfView, speed * Time.deltaTime);

            if (camera.fieldOfView <= basicFieldOfView)
                isDecreases = false;
        }
    }

    public void SetBasicFieldOfView()
    {
        camera.fieldOfView = basicFieldOfView;
    }

    public void SetBossFightFieldOfView()
    {
        camera.fieldOfView = bossFightFieldOfView;
    }

    public void SmoothlySetBossFightFieldOfVision()
    {
        isIncreases = true;
        isDecreases = false;
    }

    public void SmoothlySetBasicFieldOfVision()
    {
        isDecreases = true;
        isIncreases = false;
    }
}
