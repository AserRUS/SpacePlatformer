using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private enum CollectibleItemType
    {
        Star,
        Money
    }

    [SerializeField] private CollectibleItemType m_CollectibleItemType;
    [SerializeField] private GameObject m_SelectionEffect;

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();
        if (player != null)
        {
            if (m_CollectibleItemType == CollectibleItemType.Star)
                LevelProgress.Instance.AddStar();
            else if (m_CollectibleItemType == CollectibleItemType.Money)
                LevelProgress.Instance.AddMoney();

            Instantiate(m_SelectionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
          
        }
    }    
}
