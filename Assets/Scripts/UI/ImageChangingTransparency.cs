using UnityEngine;
using UnityEngine.UI;

public class ImageChangingTransparency : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int minTransparency = 30;
    [Range(0, 100)]
    [SerializeField] private int maxTransparency = 100;
    
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
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
    }

    public void AddTransparency()
    {
        SetAlpha(minTransparency);
    }
}
