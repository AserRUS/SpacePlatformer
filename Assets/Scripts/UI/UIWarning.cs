using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIWarning : MonoBehaviour
{
    [SerializeField] private float timeWarningDuration;
    [SerializeField] private Color warningColor;

    [Header("Images")]
    [SerializeField] private Image cornerImage;
    [SerializeField] private Image shortSideImage;
    [SerializeField] private Image longSideImage;

    private Coroutine coroutine;

    private void Start()
    {
        HideAllImage();
        cornerImage.color = warningColor;
        shortSideImage.color = warningColor;
        longSideImage.color = warningColor;
    }

    public void EnableWarning(Vector2 target, Vector2 gameobjectPosition)
    {
        Vector3 direction = target - gameobjectPosition;
        int angle = (int)(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if (angle < 0)
            angle += 360;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            HideAllImage();
        }

        switch (angle)
        {
            case >= 0 and < 30:
                shortSideImage.rectTransform.localScale = new Vector3(1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, shortSideImage.gameObject));
                break;
            case >= 30 and < 60:
                cornerImage.rectTransform.localScale = new Vector3(1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, cornerImage.gameObject));
                break;
            case >= 60 and < 120:
                longSideImage.rectTransform.localScale = new Vector3(1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, longSideImage.gameObject));
                break;
            case >= 120 and < 150:
                cornerImage.rectTransform.localScale = new Vector3(-1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, cornerImage.gameObject));
                break;
            case >= 150 and < 210:
                shortSideImage.rectTransform.localScale = new Vector3(-1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, shortSideImage.gameObject));
                break;
            case >= 210 and < 240:
                cornerImage.rectTransform.localScale = new Vector3(-1, -1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, cornerImage.gameObject));
                break;
            case >= 240 and < 300:
                longSideImage.rectTransform.localScale = new Vector3(1, -1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, longSideImage.gameObject));
                break;
            case >= 300 and < 330:
                cornerImage.rectTransform.localScale = new Vector3(1, -1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, cornerImage.gameObject));
                break;
            case >= 330 and < 360:
                shortSideImage.rectTransform.localScale = new Vector3(1, 1, 1);
                coroutine = StartCoroutine(DisableVignetteAfterTime(timeWarningDuration, shortSideImage.gameObject));
                break;
        }
    }

    private IEnumerator DisableVignetteAfterTime(float time, GameObject image)
    {
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        image.gameObject.SetActive(false);
    }

    private void HideAllImage()
    {
        cornerImage.gameObject.SetActive(false);
        shortSideImage.gameObject.SetActive(false);
        longSideImage.gameObject.SetActive(false);
    }
}
