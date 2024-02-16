using UnityEngine;
using UnityEngine.UI;

public class LevelActivator : MonoBehaviour
{
    public bool IsActived => m_IsActived;

    [SerializeField] private UILevelButton m_LevelButton;
    [SerializeField] private LevelActivator m_LastLevel;
    [SerializeField] private LevelInfo m_LevelInfo;
    [SerializeField] private Text m_StarText;
    [SerializeField] private GameObject m_ClosingImage;

    private LevelProgressManager.LevelState levelState;
    
    private bool m_IsActived = false;
    

      
    

    public void LevelActive()
    {
        levelState = LevelProgressManager.Instance.GetLevelState(m_LevelInfo);

        m_LevelButton.SetLevelInfo(m_LevelInfo);

        m_IsActived = levelState.State;
        
        m_LevelButton.Interactable = m_LastLevel == null || m_LastLevel.IsActived;

        m_ClosingImage.SetActive(!(m_LastLevel == null || m_LastLevel.IsActived));
        m_StarText.text = levelState.StarCount.ToString();        
    }
}
