using UnityEngine;

public class FORTEST_collectibleItamsCounter : MonoBehaviour
{
    private void Start()
    {
        var collectibleItems = FindObjectsOfType<CollectibleItem>();
        Debug.Log($"Count of collectibleItem in this scene: {collectibleItems.Length}");
    }
}
