using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField, Range(0, 25)] float moveSpeed = 5f;

    private Vector2 position;
    private bool isFacingRight = true;

    private InputController inputController;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetRigidbody2DSettings();
    }

    void FixedUpdate()
    {
        position.x = inputController.MovementValue.x;
        position.y = inputController.MovementValue.y;

        Move(position);
        HandleFlip(position);
    }

    private void Move(Vector2 direction)
    {
        rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
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

    private void SetRigidbody2DSettings()
    {
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidBody.gravityScale = 0;
        rigidBody.angularDrag = 0;
    }
}
