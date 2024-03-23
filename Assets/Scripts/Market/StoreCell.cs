using UnityEngine;
using UnityEngine.UI;


public class StoreCell : MonoBehaviour
{
    
    public SkinInfo SkinInfo => m_SkinInfo;

    [SerializeField] private Market m_Market;
    [SerializeField] private SkinInfo m_SkinInfo;
    [SerializeField] private Image m_SkinImage;
    [SerializeField] private Text m_CostText;
    [SerializeField] private Button m_CellButton;
    [SerializeField] private Image m_ButtonImage;
    [SerializeField] private Sprite m_SelectedButtonImage;
    [SerializeField] private Sprite m_UnselectedButtonImage;
    [SerializeField] private GameObject m_Cost;
    public void Initialize()
    {
        if (m_SkinInfo.ProductImage != null)
            m_SkinImage.sprite = m_SkinInfo.ProductImage;
        m_CostText.text = m_SkinInfo.Cost.ToString();
    }

    public void TryBuy()
    {
        m_Market.TryBuy(m_SkinInfo);
    }        

    public void SetSelected(bool isSelected)
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
    public void CheckCost()
    {
        m_CellButton.interactable = m_Market.MoneyCount >= m_SkinInfo.Cost;
    }
}

