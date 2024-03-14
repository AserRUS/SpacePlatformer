using UnityEngine;

public class UIInstructionPanel : MonoBehaviour
{
    [SerializeField] private bool openPanel;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private Pauser pauser;

    private void Start()
    {
        instructionPanel.SetActive(false);
    }

    public void OpenInstructionPanel()
    {
        if (!openPanel) return;

        instructionPanel.SetActive(true);
        pauser.Pause();
    }

    public void CloseInstructionPanel()
    {
        instructionPanel.SetActive(false);
        pauser.UnPause();
    }
}
