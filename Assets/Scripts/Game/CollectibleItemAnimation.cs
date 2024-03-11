using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItemAnimation : MonoBehaviour
{

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Amplitude;
    [SerializeField] private float m_RotateSpeed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + Mathf.Sin(m_Speed * Time.time) * m_Amplitude, startPosition.z);
        transform.Rotate(0, 1 * m_RotateSpeed * Time.deltaTime, 0);
    }
}
