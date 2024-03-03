using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class EvilDoctorBoxSpawner : EntitySpawner
{
    [SerializeField] private int minBoxCount;
    [SerializeField] private int maxBoxCount;

    private Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<Collider>().bounds;
    }

    private void ChooseSpawnPosition(out List<Vector3> boxPositions)
    {
        int boxCount = Random.Range(minBoxCount, maxBoxCount + 1);

        float step = bounds.extents.x * 2 / boxCount;
        float posY = transform.position.y;
        boxPositions = new List<Vector3>();

        if (boxCount % 2 == 0)
        {
            float firstPosXRight = transform.position.x + step / 2;
            Vector3 pos = new Vector3(firstPosXRight, posY, 0);
            boxPositions.Add(pos);

            float firstPosXLeft = transform.position.x - step / 2;
            pos = new Vector3(firstPosXLeft, posY, 0);
            boxPositions.Add(pos);

            int count = boxCount / 2 - 1;
            float posX;

            for (int i = 1; i <= count; i++)
            {
                posX = firstPosXRight + i * step;
                pos = new Vector3(posX, posY, 0);
                boxPositions.Add(pos);

                posX = firstPosXLeft - i * step;
                pos = new Vector3(posX, posY, 0);
                boxPositions.Add(pos);
            }
        }
        else
        {
            float firstPosX = transform.position.x;
            Vector3 pos = new Vector3(firstPosX, posY, 0);
            boxPositions.Add(pos);

            int count = boxCount / 2;
            float posX;

            for (int i = 1; i <= count; i++)
            {
                posX = firstPosX + i * step;
                pos = new Vector3(posX, posY, 0);
                boxPositions.Add(pos);

                posX = firstPosX - i * step;
                pos = new Vector3(posX, posY, 0);
                boxPositions.Add(pos);
            }
        }
    }

    protected override void EntitySpawn()
    {
        List<Vector3> boxPositions = new List<Vector3>();
        ChooseSpawnPosition(out boxPositions);

        GameObject boxPrefab = m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)];

        foreach (var box in boxPositions)
        {
            GameObject entity = Instantiate(boxPrefab,
            box, Quaternion.identity);
        }
    }

    public void Attack()
    {
        EntitySpawn();
    }
}
