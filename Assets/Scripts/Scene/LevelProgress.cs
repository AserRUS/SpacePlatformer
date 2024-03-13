using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance;

    [SerializeField] private UIFinish m_UIFinish;    
           
    private int starCount;
    private int moneyCount;

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
        MarketManager.Instance.AddMoney(moneyCount);
        m_UIFinish.Finish(starCount, moneyCount);
    }
       

    public void AddStar()
    {
        starCount++;
    }
    public void AddMoney()
    {
        moneyCount++;
    }
}
