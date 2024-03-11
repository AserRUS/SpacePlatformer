using System;
using System.IO;
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public const string filename = "LevelProgress";

    [Serializable]
    public class LevelState
    {
        public LevelInfo LevelInfo;
        public bool State;
        public int StarCount;

        public LevelState() {}
    }



    
    public LevelInfo CurrentLevel => m_CurrentLevel;

    public static LevelProgressManager Instance;

    public LevelState[] LevelStates => m_LevelStates;


    [SerializeField] private LevelState[] m_LevelStates;


    private LevelInfo m_CurrentLevel;

    

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Saver<LevelState[]>.TryLoad(filename, ref m_LevelStates);

    }    

    

    public void SetCurrentLevel(LevelInfo levelInfo)
    {
        m_CurrentLevel = levelInfo;
    }
    

    public void LevelFinished(int starCount)
    {
        for (int i = 0; i < m_LevelStates.Length; i++)
        {
            if (m_LevelStates[i].LevelInfo == m_CurrentLevel)
            {
                m_LevelStates[i].State = true;

                if (m_LevelStates[i].StarCount < starCount)
                    m_LevelStates[i].StarCount = starCount; 

                
            }
        }

        Saver<LevelState[]>.Save(filename, m_LevelStates);
    }
    


    public LevelInfo GetNextLevel()
    {
        for (int i = 0; i < m_LevelStates.Length; ++i)
        {
            if (m_LevelStates[i].LevelInfo == m_CurrentLevel)
            {
                if (i + 1 < m_LevelStates.Length)
                {                    
                    return m_LevelStates[i + 1].LevelInfo;
                } 
            }
        }
        return null;
    }


    public LevelState GetLevelState(LevelInfo levelInfo)
    {
        for (int i = 0; i < m_LevelStates.Length; i++)
        {
            if (levelInfo == m_LevelStates[i].LevelInfo)
            {
                return m_LevelStates[i];
            }
        }
        return null;
    }

    public LevelState GetCurrentLevelState()
    {
        if (m_CurrentLevel == null) return null;

        for (int i = 0; i < m_LevelStates.Length; i++)
        {
            if (m_CurrentLevel == m_LevelStates[i].LevelInfo)
            {
                return m_LevelStates[i];
            }
        }
        return null;
    }


    

    public void Remove()
    {
        FileHandler.Reset("Market");
        FileHandler.Reset("LevelProgress");
    }

    
}
