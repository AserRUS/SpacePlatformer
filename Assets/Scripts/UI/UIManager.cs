using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelManager m_LevelManager;

    [SerializeField] private GameObject m_MenuPanel;
    [SerializeField] private GameObject m_InterfacePositionPanel;

    private void Start()
    {
        m_MenuPanel.SetActive(PlayerPrefs.GetInt("InterfaceLayout", -1) != -1);
        m_InterfacePositionPanel.SetActive(!m_MenuPanel.activeSelf);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        LevelProgressManager.Instance.Remove();
        m_LevelManager.ActivateLevels();
    }
}
