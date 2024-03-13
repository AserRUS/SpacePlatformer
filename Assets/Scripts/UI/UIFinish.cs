using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFinish : MonoBehaviour
{
    [SerializeField] private GameObject m_FinishPanel;
    [SerializeField] private Image[] m_StarImages;
    [SerializeField] SceneLoader m_SceneLoader;
    [SerializeField] private GameObject m_NextLevelButton;
    [SerializeField] private Text m_MoneyText;
    [SerializeField] private Market m_Market;

    private LevelInfo m_NextLevel;


    private void Start()
    {
        m_NextLevel = LevelProgressManager.Instance.GetNextLevel();
        m_NextLevelButton.SetActive(m_NextLevel != null);
    }

    public void NextLevel()
    {
        if (m_NextLevel != null)
        {
            LevelProgressManager.Instance.SetCurrentLevel(m_NextLevel);
            m_SceneLoader.LoadScene(m_NextLevel.SceneName);
        }

    }   

    public void Finish(int starCount, int moneyCount)
    {
        m_Market.UpdateMoney();
        m_MoneyText.text = "+" + moneyCount.ToString();
        m_FinishPanel.SetActive(true);
        for (int i = 0; i < m_StarImages.Length; i++)
        {
            m_StarImages[i].enabled = starCount > 0;
            starCount--;
        }
    }
}
    
