
using TMPro;
using UnityEngine;

public class MarketAnimationController : MonoBehaviour
{
    [SerializeField] private RectTransform[] m_Cells;
    [SerializeField] private float m_CellsMovementSpeed;
    [SerializeField] [Range(0, 1)] private float m_ZoomRatio;

    private Vector3[] cellsTargetPositions;
    private int currentCellIndex = 1;
    private float step;
    private void Start()
    {
        if (m_Cells.Length > 1)
            step = Mathf.Abs(m_Cells[0].anchoredPosition.x - m_Cells[1].anchoredPosition.x);

        cellsTargetPositions = new Vector3[m_Cells.Length];

        for (int i = 0; i < cellsTargetPositions.Length; i++)
        {
            cellsTargetPositions[i] = m_Cells[i].anchoredPosition;
        }
    }
    public void MoveLeft()
    {
        if ((currentCellIndex + 1) > m_Cells.Length - 1) return;

        currentCellIndex++;        

        for (int i = 0; i < cellsTargetPositions.Length; i++)
        {
            cellsTargetPositions[i].x -= step;
        }
    }
    public void MoveRight()
    {
        if ((currentCellIndex - 1) < 0) return;

        currentCellIndex--;
        
        for (int i = 0; i < cellsTargetPositions.Length; i++)
        {
            cellsTargetPositions[i].x += step;
        }
    }

    

    private void Update()
    {
        Move();
        
    }

    private void Move()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].anchoredPosition = Vector3.MoveTowards(m_Cells[i].anchoredPosition, cellsTargetPositions[i], m_CellsMovementSpeed * Time.deltaTime);
        }
        
        for (int i = 0; i < m_Cells.Length; i++)
        {            
            m_Cells[i].transform.localScale = Vector3.one - Vector3.one * m_ZoomRatio * Mathf.Abs(m_Cells[i].anchoredPosition.x / step);                           
            
        }
        
    }
}
