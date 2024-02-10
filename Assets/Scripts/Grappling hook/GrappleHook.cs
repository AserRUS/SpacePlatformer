using System.Collections;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask grapplebleMask;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float grappleSpeed = 10f;
    [SerializeField] private float grappleShootSpeed = 20f;
    [SerializeField] private float jumpForceAfterHook = 10f;

    [HideInInspector] public bool retracting = false;

    private LineRenderer line;
    private Rigidbody2D rigidbody;
    private bool isGrappling = false;
    private Vector2 target;
    private Vector2 direction;
    private float distance;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        //Вынести в inputControll, если будем использовать
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartGrapple();
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(player.position, target,
                grappleSpeed * Time.deltaTime);

            player.position = grapplePos;
            line.SetPosition(0, player.position);

            if (Vector2.Distance(player.position, target) < 0.5f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;

                rigidbody.AddForce(Vector2.up * distance * jumpForceAfterHook, ForceMode2D.Impulse);
            }
        }
    }

    private void StartGrapple()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - player.position;

        RaycastHit2D hit = Physics2D.Raycast(player.position, direction, maxDistance, 
            grapplebleMask);

        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;
            distance = hit.distance;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, player.position);
        line.SetPosition(1, player.position);

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(player.position, target, t / time);
            line.SetPosition(0, player.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }


}
