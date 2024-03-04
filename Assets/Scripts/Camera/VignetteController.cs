using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    [SerializeField] private float timeVignetteDuration;
    [Header("PostProcessProfiles")]
    [SerializeField] private PostProcessProfile profile_Default;
    [SerializeField] private PostProcessProfile profile_RightSide;
    [SerializeField] private PostProcessProfile profile_BottomRightCorner;
    [SerializeField] private PostProcessProfile profile_BottomSide;
    [SerializeField] private PostProcessProfile profile_BottomLeftCorner;
    [SerializeField] private PostProcessProfile profile_LeftSide;
    [SerializeField] private PostProcessProfile profile_UpLeftCorner;
    [SerializeField] private PostProcessProfile profile_UpSide;
    [SerializeField] private PostProcessProfile profile_UpRightCorner;

    private PostProcessVolume postProcessVolume;

    private void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile = profile_Default;
    }

    public void EnableVignette(Vector2 target, Vector2 gameobjectPosition)
    {
        Vector3 direction = target - gameobjectPosition;
        int angle = (int)(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if (angle < 0)
            angle += 360;
                
        switch (angle)
        {
            case  >= 0 and < 30:
                //Debug.Log($"от 0 до 30. angle: {angle}");
                postProcessVolume.profile = profile_RightSide;
                break;
            case >= 30 and < 60:
                //Debug.Log($"от 30 до 60. angle: {angle}");
                postProcessVolume.profile = profile_UpRightCorner;
                break;
            case >= 60 and < 120:
                //Debug.Log($"от 60 до 120. angle: {angle}");
                postProcessVolume.profile = profile_UpSide;
                break;
            case >= 120 and < 150:
                //Debug.Log($"от 120 до 150. angle: {angle}");
                postProcessVolume.profile = profile_UpLeftCorner;
                break;
            case >= 150 and < 210:
                //Debug.Log($"от 150 до 210. angle: {angle}");
                postProcessVolume.profile = profile_LeftSide;
                break;
            case >= 210 and < 240:
                //Debug.Log($"от 210 до 240. angle: {angle}");
                postProcessVolume.profile = profile_BottomLeftCorner;
                break;
            case >= 240 and < 300:
                //Debug.Log($"от 240 до 300. angle: {angle}");
                postProcessVolume.profile = profile_BottomSide;
                break;
            case >= 300 and < 330:
                //Debug.Log($"от 300 до 330. angle: {angle}");
                postProcessVolume.profile = profile_UpRightCorner;
                break;
            case >= 330 and < 360:
                //Debug.Log($"от 330 до 360. angle: {angle}");
                postProcessVolume.profile = profile_RightSide;
                break;
        }

        StartCoroutine(DisableVignetteAfterTime(timeVignetteDuration));
    }

    private IEnumerator DisableVignetteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        postProcessVolume.profile = profile_Default;
    }
}
