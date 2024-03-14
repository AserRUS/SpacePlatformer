using UnityEngine;

public class UIScreenButtonPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonsPanels;
    [SerializeField] private InputControl inputControl;

    private void Start()
    {
        inputControl.InputControlStateChanged += ChangeStateButtons;
        ChangeStateButtons(false);
    }

    private void OnDestroy()
    {
        inputControl.InputControlStateChanged -= ChangeStateButtons;
    }

    private void ChangeStateButtons(bool value)
    {
        foreach (var panel in buttonsPanels)
        {
            panel.SetActive(value);
        }
    }
}
