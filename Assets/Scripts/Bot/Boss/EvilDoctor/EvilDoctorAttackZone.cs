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

    //bounds
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    private void Start()
    {
        boxSpawner = GetComponent<EvilDoctorBoxSpawner>();
        attackZoneCollider = GetComponent<BoxCollider>();

        bounds = actionAttackZoneBoundary.bounds;
        CalculateBoundaries();
    }

    public void Attack(GameObject player)
    {
        ChangePosition(player);
        boxSpawner.Attack(timeWarningAttack);
    }

    private void ChangePosition(GameObject player)
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + height,
            player.transform.position.z);
        
        if (pos.x < minX) pos.x = minX; 
        if (pos.x > maxX) pos.x = maxX;
        
        if (pos.y < minY) pos.y = minY;
        if (pos.y > maxY) pos.y = maxY;
        
        gameObject.transform.position = pos;
    }

    private void CalculateBoundaries()
    {
        maxX = bounds.center.x + bounds.extents.x - attackZoneCollider.bounds.extents.x;
        minX = bounds.center.x - bounds.extents.x + attackZoneCollider.bounds.extents.x;
        maxY = bounds.center.y + bounds.extents.y - attackZoneCollider.bounds.extents.y;
        minY = bounds.center.y - bounds.extents.y + attackZoneCollider.bounds.extents.y;
    }
}
