using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{

    public const string MoneyFilename = "Money";

    public int MoneyCount => moneyCount;


    [SerializeField] private Text m_MoneyText;
    [SerializeField] private StoreCell[] m_Cells;


    private int moneyCount;

    private void Start()
    {  
        foreach (var slot in m_Cells)
        {
            slot.Initialize();
        }

        Saver<int>.TryLoad(MoneyFilename, ref moneyCount);

        UpdateMoney();
        UpdateCells();
    }
    

    public void TryBuy(SkinInfo skinInfo)
    {
        if (SkinsManager.Instance.GetSkinState(skinInfo).IsOpened == true)
        {
            Select(skinInfo);
        }
        else
        {
            Buy(skinInfo);
        }        
    }
    public void Buy(SkinInfo skinInfo)
    {        
        SkinsManager.Instance.Open(skinInfo);
        RemoveMoney(skinInfo.Cost);
        UpdateCells();
    }
    public void Select(SkinInfo skinInfo)
    {
        SkinsManager.Instance.Select(skinInfo);
        UpdateCells();
    }

    public void RemoveMoney(int moneyCount)
    {
        this.moneyCount -= moneyCount;
        if (this.moneyCount < 0)
            this.moneyCount = 0;
        UpdateMoney();
        Saver<int>.Save(MoneyFilename, this.moneyCount);
    }
    public void AddMoney(int moneyCount)
    {
        this.moneyCount += moneyCount;
        UpdateMoney();
        Saver<int>.Save(MoneyFilename, moneyCount);
    }

    public void UpdateMoney()
    {        
        m_MoneyText.text = moneyCount.ToString();
        foreach (var slot in m_Cells)
        {
            if (SkinsManager.Instance.GetSkinState(slot.SkinInfo).IsOpened == false)
                slot.CheckCost();
        }
    }

    public void UpdateCells()
    {
        foreach (var slot in m_Cells)
        {
            if (SkinsManager.Instance.GetSkinState(slot.SkinInfo).IsOpened == true)
                slot.SetSelected(SkinsManager.Instance.GetSkinState(slot.SkinInfo).IsSelected);
        }
    }
}


