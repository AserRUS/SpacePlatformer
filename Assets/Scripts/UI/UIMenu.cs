using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button m_ContinueButton;
    [SerializeField] private LevelManager m_LevelManager;

    private void Start()
    {
        m_ContinueButton.interactable = LevelProgressManager.Instance.Progress();
    }

    public void NewGame()
    {
        LevelProgressManager.Instance.Remove();
        m_ContinueButton.interactable = false;
        m_LevelManager.ActivateLevels();
    }
}
