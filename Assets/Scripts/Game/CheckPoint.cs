using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckPoint : MonoBehaviour
{    
    [SerializeField] PlayerSpawner m_Spawner;
    [SerializeField] private Transform m_NewSpawnPosition;
    [SerializeField] private MeshRenderer[] m_MeshRenderers;
    [SerializeField] private Material replacedMaterial;

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();
        if (player != null)
        {
            foreach (MeshRenderer meshRenderer in m_MeshRenderers)
            {
                meshRenderer.material = replacedMaterial;
            }

            m_NewSpawnPosition.position = new Vector3(m_NewSpawnPosition.position.x, m_NewSpawnPosition.position.y, 0);
            m_Spawner.SetSpawnPosition(m_NewSpawnPosition);
            Destroy(this);            
        }
    }
}
