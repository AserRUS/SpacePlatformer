using System;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public const string SkinFilename = "Market";

    public static SkinsManager Instance;
     

    [Serializable]
    public class SkinState
    {
        public SkinInfo m_SkinInfo;
        public bool IsOpened = false;
        public bool IsSelected = false;
    }
    [SerializeField] private SkinState[] m_SkinStates;


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

        Saver<SkinState[]>.TryLoad(SkinFilename, ref m_SkinStates);
    }

    public void Open(SkinInfo skinInfo)
    {
        for (int i = 0; i < m_SkinStates.Length; i++)
        {
            if (skinInfo == m_SkinStates[i].m_SkinInfo)
            {
                m_SkinStates[i].IsOpened = true;
            }
        }
        Saver<SkinState[]>.Save(SkinFilename, m_SkinStates);
        
    }

    public void Select(SkinInfo productInfo)
    {
        for (int i = 0; i < m_SkinStates.Length; i++)
        {
            if (productInfo == m_SkinStates[i].m_SkinInfo)
            {
                m_SkinStates[i].IsSelected = true;
            }
            else
            {
                m_SkinStates[i].IsSelected = false;
            }
        }
        Saver<SkinState[]>.Save(SkinFilename, m_SkinStates);

    }
    
    public SkinState GetSkinState(SkinInfo productInfo)
    {
        for (int i = 0; i < m_SkinStates.Length; i++)
        {
            if (productInfo == m_SkinStates[i].m_SkinInfo)
            {
                return m_SkinStates[i];
            }
        }
        return null;
    }

    public Material GetSkinMaterial()
    {
        for (int i = 0; i < m_SkinStates.Length; i++)
        {
            if(m_SkinStates[i].IsSelected == true)
            {
                return m_SkinStates[i].m_SkinInfo.m_SkinMaterial;
            }
        }
        return null;
    }
}
