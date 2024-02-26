using Unity.VisualScripting;
using UnityEngine;

public class UIOneButtonPressController : MonoBehaviour
{
    private UIButton[] buttons;

    private bool Interactable = true;
    public void SetInteractable(bool interactable) => Interactable = interactable;

    private void Start()
    {
        buttons = GetComponentsInChildren<UIButton>();

        if (buttons == null)
            Debug.LogError("Button list is empty.");

        foreach (UIButton button in buttons)
        {
            button.OnPointerDownEvent.AddListener(OnButtonDown);
            button.OnPointerUpEvent.AddListener(OnButtonUp);
        }
    }

    private void OnDestroy()
    {
        foreach (UIButton button in buttons)
        {
            button.OnPointerDownEvent.RemoveListener(OnButtonDown);
            button.OnPointerUpEvent.RemoveListener(OnButtonUp);
        }
    }

    private void OnButtonDown(UIButton button)
    {
        CloseButtons(button);
    }

    private void OnButtonUp(UIButton button)
    {
        OpenButtons(button);
    }

    private void CloseButtons(UIButton selectButton)
    {
        if (Interactable == false) return;

        foreach (UIButton button in buttons)
        {
            if (button == selectButton) continue;

            button.Interactable = false;
            UIImageChangingTransparency changingTransparency = button.GetComponent<UIImageChangingTransparency>();

            if (changingTransparency)
            {
                changingTransparency.AddTransparency();
            }
        }
    }

    private void OpenButtons(UIButton selectButton)
    {
        if (Interactable == false) return;

        foreach (UIButton button in buttons)
        {
            if (button == selectButton) continue;

            button.Interactable = true;
            UIImageChangingTransparency changingTransparency = button.GetComponent<UIImageChangingTransparency>();

            if (changingTransparency)
            {
                changingTransparency.RemoveTransparency();
            }
        }
    }
}

