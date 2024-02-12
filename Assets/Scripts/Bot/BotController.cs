using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private GameObject flipModel;

    [SerializeField] private float speed;

    [Header("BotType")]
    [SerializeField] private BotType botType;
    [Header("if type Stand")]
    [SerializeField] private bool isFacingRight = true;
    [Header("If type partol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float infelicity = 0.1f;

    private Rigidbody rb;
    private BotWeapon botWeapon;
    private Transform targetPoint;
    private Transform detectedPlayer;
    private BotBehavior behavior;
    private bool facingRight = true;
    private bool detected = false;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        botWeapon = GetComponentInChildren<BotWeapon>();

        if (botType == BotType.patrolling)
        {
            targetPoint = patrolPoints[0];
        }
        ChangeBehavior();
    }

    private void Update()
    {
        ChangeBehavior();
        ChooseBehaviour();
    }

    private void ChooseBehaviour()
    {
        if (behavior == BotBehavior.patrol)
        {
            Patrol();
        }

        if (behavior == BotBehavior.attack)
        {
            Attack();
        }
    }

    private void ChangeBehavior()
    {
        if (detected)
        {
            behavior = BotBehavior.attack;
        }
        else
        {
            if (botType == BotType.standing)
            {
                behavior = BotBehavior.stand;
                if (isFacingRight != facingRight)
                    Flip();
            }
            if (botType == BotType.patrolling)
            {
                behavior = BotBehavior.patrol;
                CheckRotation(targetPoint);
            }
        }
    }

    private void Patrol()
    {
        float distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
            new Vector2(targetPoint.position.x, targetPoint.position.y));

        if (distance <= infelicity)
        {
            SetNextTargetPoint();
            CheckRotation(targetPoint);
        }

        if (!facingRight)
        {
            rb.velocity = new Vector3(speed * (-1), rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        }
    }

    private void Attack()
    {
        CheckRotation(detectedPlayer);
        botWeapon.Attack();
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = flipModel.transform.localScale;
        scale.z *= -1;
        flipModel.transform.localScale = scale;
    }

    private void SetNextTargetPoint()
    {
        if (targetPoint == patrolPoints[0])
            targetPoint = patrolPoints[1];
        else
            targetPoint = patrolPoints[0];
    }

    private void CheckRotation(Transform target)
    {
        if (target.position.x < transform.position.x && facingRight)
            Flip();
        else if (target.position.x > transform.position.x && !facingRight)
            Flip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            detectedPlayer = other.transform;
            detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectedPlayer = null;
            detected = false;
        }
    }
}
