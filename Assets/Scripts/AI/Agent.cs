using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] float warningRange = 5f;
    [SerializeField] float maxVelocity;
    [SerializeField] float maxForce;
    [SerializeField] float avoidObstacleRayDistance;
    [SerializeField] LayerMask obstaclesLayer;
    [SerializeField] Rigidbody2D rb;

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

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, rb.velocity * 5f);
    }
    Vector2 desiredVelocity;
    Vector2 steeringVelocity;
    private bool isFacingRight = true;

    private void FixedUpdate()
    {
        HandleFlip(rb.velocity.normalized);
        /*   RaycastHit2D rayHit = Physics2D.Raycast(transform.position, rb.velocity.normalized, warningRange, obstaclesLayer);
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
           }*/

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

        // rb.velocity += (Vector2)RandomDirection() * Time.fixedDeltaTime;
    }

    private void Flee()
    {
        desiredVelocity = (transform.position - player.position).normalized;
        desiredVelocity *= maxVelocity;

        steeringVelocity = desiredVelocity - rb.velocity;
        steeringVelocity = Vector2.ClampMagnitude(steeringVelocity, maxForce);

        rb.velocity += steeringVelocity;
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        // Applica la steering velocity al Rigidbody
        rb.AddForce(steeringVelocity, ForceMode2D.Force);

        // Limita la velocità del Rigidbody
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        // transform.position += (Vector3)rb.velocity * Time.fixedDeltaTime;
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
}
