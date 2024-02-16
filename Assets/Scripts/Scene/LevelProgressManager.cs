using System;
using System.IO;
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
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
        Load();
        
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

                m_LevelStates[i].StarCount = starCount; 

                Save();
            }
        }       

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


    [Serializable]
    private class Saver
    {
        public LevelState[] LevelStates;
    }
    private void Save()
    {
        Saver saver = new Saver();
        saver.LevelStates = m_LevelStates;
        string dataString = JsonUtility.ToJson(saver);        
        File.WriteAllText(Application.persistentDataPath + "/" + "LevelProgress", dataString);
    }

    private void Load()
    {
        Debug.Log(Application.persistentDataPath);

        if (File.Exists(Application.persistentDataPath + "/" + "LevelProgress"))
        {
            string dataString = File.ReadAllText(Application.persistentDataPath + "/" + "LevelProgress");
            Saver saver = new Saver();
            saver = JsonUtility.FromJson<Saver>(dataString);
            m_LevelStates = saver.LevelStates;
        }
    }

    public void Remove()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "LevelProgress"))
        {
            File.Delete(Application.persistentDataPath + "/" + "LevelProgress");

            for (int i = 0; i < m_LevelStates.Length; i++)
            {
                m_LevelStates[i].State = false;
                m_LevelStates[i].StarCount = 0;

            }
        }
    }

    public bool Progress()
    {
        return File.Exists(Application.persistentDataPath + "/" + "LevelProgress");
    }
}