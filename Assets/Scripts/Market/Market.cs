using UnityEngine;
using UnityEngine.UI;
using System;

public class Market : MonoBehaviour
{ 
    private int m_Money;

    [SerializeField] private Text m_MoneyText;
    [SerializeField] private StoreCell[] m_Cells;    

    private void Start()
    {           

        foreach (var slot in m_Cells)
        {
            slot.Initialize();
            slot.BuyButton.onClick.AddListener(UpdateMoney);
        }
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        m_Money = MarketManager.Instance.MoneyCount;
        m_MoneyText.text = m_Money.ToString();
        foreach (var slot in m_Cells)
        {
            slot.CheckCost();
        }
    }

   
}


