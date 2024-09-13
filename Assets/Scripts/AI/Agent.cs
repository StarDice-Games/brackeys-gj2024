using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Agent : MonoBehaviour
{
    [SerializeField] float warningRange = 5f;
    [SerializeField] float maxVelocity;
    [SerializeField] float maxForce;
    [SerializeField] float avoidObstacleRayDistance;
    [SerializeField] LayerMask obstaclesLayer;
    Rigidbody2D rb;

    public Vector2 areaMin;
    public Vector2 areaMax;

    public Transform ray;
    Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, warningRange);
    }
    Vector2 desiredVelocity;
    Vector2 steeringVelocity;
    private bool isFacingRight;

    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, rb.velocity.normalized, warningRange, obstaclesLayer);
        Debug.DrawRay(transform.position, rb.velocity.normalized * warningRange, Color.green);
        if (rayHit)
        {
            Debug.Log("Collidendo con.. " + rayHit.transform);

            if (rayHit.transform != gameObject.transform)
            {
                Debug.Log("Collidendo con.. " + rayHit.transform);
                if (Vector2.Distance(transform.position, rayHit.transform.position) < 2f)
                {
                    desiredVelocity = (transform.position - (Vector3)rayHit.point).normalized;
                    desiredVelocity *= maxVelocity;

                    steeringVelocity = desiredVelocity - rb.velocity;
                    steeringVelocity = Vector2.ClampMagnitude(steeringVelocity, maxForce);

                    rb.velocity += steeringVelocity;
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

                    transform.position += (Vector3)rb.velocity * Time.fixedDeltaTime;
                }
            }
        }

        RaycastHit2D warningCircleHit = Physics2D.CircleCast(transform.position, warningRange, Vector2.right);
        if (warningCircleHit)
        {
            if (warningCircleHit.transform.gameObject.CompareTag("Player"))
            {
                player = warningCircleHit.transform;
                Debug.Log("RUUUNNNN");
            }
            else
            {
                Debug.Log("Player is far");
                player = null;
            }
        }

        if (player)
        {
            if (Vector2.Distance(transform.position, player.position) < warningRange)
            {
                desiredVelocity = (transform.position - player.position).normalized;
                desiredVelocity *= maxVelocity;

                steeringVelocity = desiredVelocity - rb.velocity;
                steeringVelocity = Vector2.ClampMagnitude(steeringVelocity, maxForce);

                rb.velocity += steeringVelocity;
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

                transform.position += (Vector3)rb.velocity * Time.fixedDeltaTime;
            }
            else
            {
            }
        }

        //HandleFlip(rb.velocity.normalized);
    }

    private Vector2 RandomTarget()
    {
        float randomX = Random.Range(areaMin.x, areaMax.x);
        float randomY = Random.Range(areaMin.y, areaMax.y);
        return new Vector2(randomX, randomY);
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
}
