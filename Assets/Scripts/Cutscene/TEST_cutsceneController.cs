
using UnityEngine;

public class TEST_cutsceneController : MonoBehaviour
{
    [SerializeField] private CameraController mainCameraController;
    [SerializeField] private CutsceneManager cutsceneManager;

    [Header("Cutscene")]
    [SerializeField] private GameObject cutscene;
    [SerializeField] private CameraMode cameraModeAfterCutsceneEnd = CameraMode.FollowMode;

    private void Start()
    {
        cutsceneManager.OnCutsceneEnd += OnCutsceneEnd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCutscene();
        }
    }

    private void OnDestroy()
    {
        cutsceneManager.OnCutsceneEnd -= OnCutsceneEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCutscene();
    }

    private void StartCutscene()
    {
        mainCameraController.SetMode(CameraMode.StandMode);
        CutsceneManager.Instance.StartCutscene(cutscene);
    }

    private void OnCutsceneEnd()
    {
        mainCameraController.SetMode(cameraModeAfterCutsceneEnd);
    }
}
