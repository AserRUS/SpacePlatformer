using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EvilDoctorAttackZone : MonoBehaviour
{
    [SerializeField] private Collider actionAttackZoneBoundary;
    [SerializeField] private float height;
    [SerializeField] private float timeWarningAttack;

    private GameObject player;
    private EvilDoctorBoxSpawner boxSpawner;
    private BoxCollider attackZoneCollider;
    private Bounds bounds;
    private bool readyToAttack;

    //bounds
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    //temporarily
    private MeshRenderer meshRenderer;

    private void Start()
    {
        boxSpawner = GetComponent<EvilDoctorBoxSpawner>();
        attackZoneCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        bounds = actionAttackZoneBoundary.bounds;
        //temporarily
        readyToAttack = true;

        CalculateBoundaries();
    }

    public void Attack()
    {
        if (!readyToAttack) return;

        meshRenderer.enabled = true;
        readyToAttack = false;
        ChangePosition();

        StartCoroutine(WaitWarningAttackTime(timeWarningAttack));
    }

    private void ChangePosition()
    {
        //TODO: заменить FindGameObjectWithTag 
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + height,
            player.transform.position.z);
        
        if (pos.x < minX) pos.x = minX; 
        if (pos.x > maxX) pos.x = maxX;
        
        if (pos.y < minY) pos.y = minY;
        if (pos.y > maxY) pos.y = maxY;
        
        gameObject.transform.position = pos;
    }

    private IEnumerator WaitWarningAttackTime(float time)
    {
        yield return new WaitForSeconds(time);

        meshRenderer.enabled = false;
        readyToAttack = true;
        boxSpawner.Attack();
    }

    private void CalculateBoundaries()
    {
        maxX = bounds.center.x + bounds.extents.x - attackZoneCollider.bounds.extents.x;
        minX = bounds.center.x - bounds.extents.x + attackZoneCollider.bounds.extents.x;
        maxY = bounds.center.y + bounds.extents.y - attackZoneCollider.bounds.extents.y;
        minY = bounds.center.y - bounds.extents.y + attackZoneCollider.bounds.extents.y;
    }
}
