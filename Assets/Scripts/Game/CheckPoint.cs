using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckPoint : MonoBehaviour
{    
    [SerializeField] PlayerSpawner m_Spawner;
    [SerializeField] private Transform m_NewSpawnPosition;
    [SerializeField] private MeshRenderer m_MeshRenderer;
        

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();
        if (player != null)
        {
            m_MeshRenderer.material.color = Color.green;
            m_Spawner.SetSpawnPosition(m_NewSpawnPosition);
            Destroy(this);            
        }
        
    }
}
