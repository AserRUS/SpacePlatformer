
using UnityEngine;
using UnityEngine.UI;

public class InterfacePositionSettings : MonoBehaviour
{
    private enum InterfaceLayout
    {
        Left,
        Right
    }

    [SerializeField] RectTransform[] m_MainUIElements;
    [SerializeField] RectTransform[] m_SecondaryUIElements;

    [SerializeField] Image m_LeftLayoutInterfaceImage;
    [SerializeField] Image m_RightLayoutInterfaceImage;

    private InterfaceLayout interfaceLayout = InterfaceLayout.Right;


    private void Start()
    {
        Load();

        if (interfaceLayout == InterfaceLayout.Right)
        {  
            Right();
        }
        else if (interfaceLayout == InterfaceLayout.Left)
        {       
            Left();
        }
    }

    private void Left()
    { 
        for (int i = 0; i < m_MainUIElements.Length; i++)
        {            
            m_MainUIElements[i].anchorMax = new Vector2(0, m_MainUIElements[i].anchorMax.y);
            m_MainUIElements[i].anchorMin = new Vector2(0, m_MainUIElements[i].anchorMin.y);

            m_MainUIElements[i].anchoredPosition = new Vector2(Mathf.Abs(m_MainUIElements[i].anchoredPosition.x), m_MainUIElements[i].anchoredPosition.y);
        }
        for (int i = 0; i < m_SecondaryUIElements.Length; i++)
        { 
            
            m_SecondaryUIElements[i].anchorMax = new Vector2(1, m_SecondaryUIElements[i].anchorMax.y);
            m_SecondaryUIElements[i].anchorMin = new Vector2(1, m_SecondaryUIElements[i].anchorMin.y);

            m_SecondaryUIElements[i].anchoredPosition = new Vector2(-Mathf.Abs(m_SecondaryUIElements[i].anchoredPosition.x), m_SecondaryUIElements[i].anchoredPosition.y);

        }
        m_LeftLayoutInterfaceImage.enabled = true;
        m_RightLayoutInterfaceImage.enabled = false;

        interfaceLayout = InterfaceLayout.Left;
        Save();
    }

    private void Right()
    {
        for (int i = 0; i < m_MainUIElements.Length; i++)
        {  
            m_MainUIElements[i].anchorMax = new Vector2(1, m_MainUIElements[i].anchorMax.y);
            m_MainUIElements[i].anchorMin = new Vector2(1, m_MainUIElements[i].anchorMin.y);

            m_MainUIElements[i].anchoredPosition = new Vector2(-Mathf.Abs(m_MainUIElements[i].anchoredPosition.x), m_MainUIElements[i].anchoredPosition.y);
        }
        for (int i = 0; i < m_SecondaryUIElements.Length; i++)
        {  
            m_SecondaryUIElements[i].anchorMax = new Vector2(0, m_SecondaryUIElements[i].anchorMax.y);
            m_SecondaryUIElements[i].anchorMin = new Vector2(0, m_SecondaryUIElements[i].anchorMin.y);

            m_SecondaryUIElements[i].anchoredPosition = new Vector2(Mathf.Abs(m_SecondaryUIElements[i].anchoredPosition.x), m_SecondaryUIElements[i].anchoredPosition.y);
        }
        m_LeftLayoutInterfaceImage.enabled = false;
        m_RightLayoutInterfaceImage.enabled = true;

        interfaceLayout = InterfaceLayout.Right;
        Save();
    }

    public void Swap()
    {
        if (interfaceLayout == InterfaceLayout.Right)
        {
            Left();
        }
        else if (interfaceLayout == InterfaceLayout.Left)
        {
            Right();
        }
    }


    private void Save()
    {
        PlayerPrefs.SetInt("InterfaceLayout", (int)interfaceLayout);
    }
    private void Load()
    {
        int index = PlayerPrefs.GetInt("InterfaceLayout", -1);

        if (index != -1)
        {
            interfaceLayout = (InterfaceLayout)index;
        }
    }
}
