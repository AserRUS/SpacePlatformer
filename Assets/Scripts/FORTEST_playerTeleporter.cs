using UnityEngine;

public class FORTEST_playerTeleporter : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;

    [Header("Instead button")]
    [SerializeField] private bool teleport = false;

    private void Update()
    {
        if (teleport)
        {
            Player player = playerSpawner.GetPlayer();
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
            player.transform.position = pos;
            teleport = false;
        }
    }
}
