using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class StoreCell : MonoBehaviour
{
    public event UnityAction BuyEvent;
    public event UnityAction SelectEvent;
    public ProductInfo ProductInfo => m_ProductInfo;

    [SerializeField] private ProductInfo m_ProductInfo;
    [SerializeField] private Image m_ProductImage;
    [SerializeField] private Text m_CostText;
    [SerializeField] private Button m_BuyButton;
    [SerializeField] private Image m_ButtonImage;
    [SerializeField] private Sprite m_SelectedButtonImage;
    [SerializeField] private Sprite m_UnselectedButtonImage;
    [SerializeField] private GameObject m_Cost;

    private bool isSold = false;
    private bool isSelected = false;
    public void Initialize()
    {
        if (m_ProductInfo.ProductImage != null)
            m_ProductImage.sprite = m_ProductInfo.ProductImage;
        m_CostText.text = m_ProductInfo.Cost.ToString();

        isSold = MarketManager.Instance.GetProductState(m_ProductInfo).IsSold;
    }

    private void Buy()
    {
        isSold = true;
        MarketManager.Instance.Buy(m_ProductInfo);
        CheckSelected();
        BuyEvent.Invoke();
    }
    private void Select()
    {
        MarketManager.Instance.Select(m_ProductInfo);
        SelectEvent.Invoke();
    }

    public void Action()
    {
        if (isSold == false)
            Buy();
        else
            Select();

    }

    public void CheckSelected()
    {        
        isSelected = MarketManager.Instance.GetProductState(m_ProductInfo).IsSelected;

        if (isSold == true)
        {
            m_Cost.SetActive(false);

            if (isSelected == true)
            {
                m_ButtonImage.sprite = m_SelectedButtonImage;
            }
            else
            {
                m_ButtonImage.sprite = m_UnselectedButtonImage;
            }
        }
    }
    public void CheckCost()
    {
        if (isSold == false)
            m_BuyButton.interactable = MarketManager.Instance.MoneyCount >= m_ProductInfo.Cost;
    }
}

