using System;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public const string MarketFilename = "Market";
    public const string MoneyFilename = "Money";

    public static MarketManager Instance;

    public int MoneyCount => moneyCount;

    [Serializable]
    public class ProductState
    {
        public ProductInfo m_ProductInfo;
        public bool IsSold = false;
    }
    [SerializeField] private ProductState[] m_ProductStates;


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
        DontDestroyOnLoad(gameObject);
        Saver<ProductState[]>.TryLoad(MarketFilename, ref m_ProductStates);
        Saver<int>.TryLoad(MoneyFilename, ref moneyCount);
    }

    public void Buy(ProductInfo productInfo)
    {
        for (int i = 0; i < m_ProductStates.Length; i++)
        {
            if (productInfo == m_ProductStates[i].m_ProductInfo)
            {
                m_ProductStates[i].IsSold = true;
                RemoveMoney(productInfo.Cost);
            }
        }
        Saver<ProductState[]>.Save(MarketFilename, m_ProductStates);
        
    }

    public void RemoveMoney(int value)
    {
        moneyCount -= value;
        if (moneyCount < 0)
            moneyCount = 0;
        Saver<int>.Save(MoneyFilename, moneyCount);
    }
    public void AddMoney(int moneyCount)
    {
        this.moneyCount += moneyCount;
        Saver<int>.Save(MoneyFilename, moneyCount);
    }
    public bool GetProductState(ProductInfo productInfo)
    {
        for (int i = 0; i < m_ProductStates.Length; i++)
        {
            if (productInfo == m_ProductStates[i].m_ProductInfo)
            {
                return m_ProductStates[i].IsSold;
            }
        }
        return false;
    }
}
