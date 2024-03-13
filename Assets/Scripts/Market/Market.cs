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
            slot.BuyEvent += UpdateMoney;
            slot.SelectEvent += UpdateCells;
        }
        UpdateMoney();
        UpdateCells();
    }
    private void OnDestroy()
    {
        foreach (var slot in m_Cells)
        {
            slot.BuyEvent -= UpdateMoney;
            slot.SelectEvent -= UpdateMoney;
        }
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

    public void UpdateCells()
    {
        foreach (var slot in m_Cells)
        {
            slot.CheckSelected();
        }
    }
}


