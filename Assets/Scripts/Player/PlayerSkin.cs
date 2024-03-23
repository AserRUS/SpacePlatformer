using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_MeshRenderer;

    private void Start()
    {
        m_MeshRenderer.material = SkinsManager.Instance.GetSkinMaterial();
    }
}
