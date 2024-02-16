using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance;

    [SerializeField] private UIFinish m_UIFinish;    
           
    private int starCount;

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
    }    
    
    public void LevelFinished()
    {        
        LevelProgressManager.Instance.LevelFinished(starCount);
        m_UIFinish.Finish(starCount);
    }
       

    public void AddStar()
    {
        starCount++;
    }
}
