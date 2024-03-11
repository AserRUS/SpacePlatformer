using UnityEngine;
using UnityEngine.UI;


public class StoreCell : MonoBehaviour
{
    public Button BuyButton => m_BuyButton;
    public ProductInfo ProductInfo => m_ProductInfo;

    [SerializeField] private ProductInfo m_ProductInfo;
    [SerializeField] private Image m_ProductImage;
    [SerializeField] private Text m_CostText;
    [SerializeField] private Button m_BuyButton;
    [SerializeField] private Text m_ButtonText;

    private bool isSold = false;
    public void Initialize()
    {
        if (m_ProductInfo.ProductImage != null)
            m_ProductImage.sprite = m_ProductInfo.ProductImage;
        m_CostText.text = m_ProductInfo.Cost.ToString();

        isSold = MarketManager.Instance.GetProductState(m_ProductInfo);

        if (isSold == true)
        {
            m_ButtonText.text = "Продано";
            m_BuyButton.interactable = false;
            m_CostText.text = "-";
        }


        
    }

    public void Buy()
    {
        MarketManager.Instance.Buy(m_ProductInfo);
        Initialize();
    }

    public void CheckCost()
    {
        if (isSold == false)
            m_BuyButton.interactable = MarketManager.Instance.MoneyCount >= m_ProductInfo.Cost;
    }
}

