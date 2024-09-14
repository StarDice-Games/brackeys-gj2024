using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private float warningRange = 5f;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float maxForce;

    private Rigidbody2D rb;
    private Item item;

    private Transform player;

    private Vector2 desiredVelocity;
    private Vector2 steeringVelocity;
    [SerializeField] bool isFacingRight = true;

    private BoxCollider2D[] boxCollider2Ds;

    [SerializeField] AnimationHandler animationHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        item = GetComponent<Item>();
        animationHandler = GetComponent<AnimationHandler>();
        boxCollider2Ds = GetComponents<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, warningRange);

        Gizmos.color = Color.magenta;

        if (rb)
        {
            Gizmos.DrawRay(transform.position, rb.velocity * 5f);
        }
    }


    private void FixedUpdate()
    {
        Debug.Log("rb.velocity " + rb.velocity);
        Debug.Log("desiredVelocity " + desiredVelocity);
        Debug.Log("steeringVelocity " + desiredVelocity);

        animationHandler.SetIsMoving(rb.velocity != Vector2.zero);

        if (item)
        {
            if (!item.IsInteractable)
            {
                Die();
                return;
            }
        }

        HandleFlip(rb.velocity.normalized);

        RaycastHit2D warningCircleHit = Physics2D.CircleCast(transform.position, warningRange, Vector2.right);
        if (warningCircleHit)
        {
            if (warningCircleHit.transform.gameObject.CompareTag("Player"))
            {
                player = warningCircleHit.transform;
            }
            else
            {
                player = null;
            }
        }

        if (player)
        {
            if (Vector2.Distance(transform.position, player.position) < warningRange)
            {
                Flee();
                return;
            }
        }
    }

    private void Flee()
    {
        desiredVelocity = (transform.position - player.position).normalized;
        desiredVelocity *= maxVelocity;

        steeringVelocity = desiredVelocity - rb.velocity;
        steeringVelocity = Vector2.ClampMagnitude(steeringVelocity, maxForce);

        rb.velocity += steeringVelocity;

        rb.AddForce(steeringVelocity, ForceMode2D.Force);

        // Limita la velocità del Rigidbody
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private Vector2 RandomDirection()
    {
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(-1, 1);

        Vector2 randomDirection = new Vector2(randomX, randomY);
        return randomDirection.normalized;
    }

    private void HandleFlip(Vector2 direction)
    {
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Die()
    {
        foreach (var collider in boxCollider2Ds)
        {
            if (!collider.isTrigger)
            {
                collider.isTrigger = true;
            }
        }

        Debug.Log("Guest died");
        rb.velocity = Vector2.zero;
        desiredVelocity = Vector2.zero;
        steeringVelocity = Vector2.zero;
    }
}
