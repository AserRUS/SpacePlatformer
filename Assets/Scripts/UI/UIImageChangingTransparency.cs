using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIImageChangingTransparency : MonoBehaviour
{
    [SerializeField] private ButtonPressDuration buttonPressDuration;

    [SerializeField] private bool startWithoutTransparency;
    [Range(0, 100)]
    [SerializeField] private int minTransparency = 30;
    [Range(0, 100)]
    [SerializeField] private int maxTransparency = 100;

    [Header("For smooth change")]
    [SerializeField] private float timeBetweenTransparencyChange = 0.1f;
    
    private Image image;
    private float time;
    private int alpha; 

    private void Start()
    {
        image = GetComponent<Image>();
        
        if (startWithoutTransparency)
            RemoveTransparency();
        else
            AddTransparency();
    }

    private void SetAlpha(int percent)
    {
        Color color = image.color;
        color.a = (float)percent / 100;
        image.color = color;
    }

    public void RemoveTransparency()
    {
        SetAlpha(maxTransparency);
        alpha = maxTransparency;
    }

    public void AddTransparency()
    {
        SetAlpha(minTransparency);
        alpha = minTransparency;
        time = buttonPressDuration.TimeLimitForButtonClamp;
    }

    public void SmoothRemoveTransparency()
    {
        float difference = maxTransparency - minTransparency;
        float step = timeBetweenTransparencyChange * difference / buttonPressDuration.TimeLimitForButtonClamp;

        StartCoroutine(Smooth(timeBetweenTransparencyChange, buttonPressDuration.TimeLimitForButtonClamp, step));
    }

    IEnumerator Smooth(float waitTime, float maxTime, float step)
    {
        if (time >= maxTime)
        {
            time = 0;
            yield return null;
        }
        
        yield return new WaitForSeconds(waitTime);

        time += waitTime;

        alpha += (int)step;
        Color color = image.color;
        color.a = (float)alpha / 100;
        image.color = color;

        if (time < maxTime)
            StartCoroutine(Smooth(waitTime, maxTime, step));
    }
}
