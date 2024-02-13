using System.Collections;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private GameObject flipModel;

    [SerializeField] private float speed;
    [SerializeField] private float timeDelayAfterLossPlayer;

    [Header("BotType")]
    [SerializeField] private BotType botType;
    [Header("if type Stand")]
    [SerializeField] private bool isFacingRight = true;
    [Header("If type partol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float timeDelayAfterReachPoint;
    [SerializeField] private float infelicity = 0.1f;

    private Rigidbody rb;
    private BotWeapon botWeapon;
    private Transform targetPoint;
    private Transform detectedPlayer;
    private bool detected = false;
    private BotBehavior behavior;
    private bool facingRight = true;

    private bool waitingPlayer = false;

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
            if (!waitingPlayer)
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
            else
            {
                behavior = BotBehavior.stand;
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
            waitingPlayer = true;
            StartCoroutine(WaitPlayer(timeDelayAfterReachPoint));
            //CheckRotation(targetPoint);
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
        botWeapon.TryAttack(facingRight);
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
            waitingPlayer = true;

            Destructible dest = other.GetComponent<Destructible>();
            if (dest)
            {
                dest.DeathEvent += PlayerDeath;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectedPlayer = null;
            detected = false;
            StartCoroutine(WaitPlayer(timeDelayAfterLossPlayer));

            Destructible dest = other.GetComponent<Destructible>();
            if (dest)
            {
                dest.DeathEvent -= PlayerDeath;
            }
        }
    }

    private void PlayerDeath()
    {
        Destructible dest = detectedPlayer.GetComponent<Destructible>();

        if (dest)
        {
            dest.DeathEvent -= PlayerDeath;
            detectedPlayer = null;
            detected = false;
        }
    }

    IEnumerator WaitPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        waitingPlayer = false;
        CheckRotation(targetPoint);
    }
}
