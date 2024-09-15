using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 25)] float moveSpeed = 5f;

    [SerializeField] Transform childToFlip;

    private Vector2 position;
    private Vector2 lastPosition;

    private bool isFacingRight = true;
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private InputController inputController;
    private Rigidbody2D rigidBody;
    private InteractionDetector interactorDetector;
    private AnimationHandler animHandler;

    public Vector2 LastPosition { get => lastPosition; }

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        rigidBody = GetComponent<Rigidbody2D>();
        interactorDetector = GetComponent<InteractionDetector>();
        animHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        SetRigidbody2DSettings();
        inputController.Interact += interactorDetector.Interact;
    }

    void FixedUpdate()
    {
        position.x = inputController.MovementValue.x;
        position.y = inputController.MovementValue.y;

        animHandler.SetIsMoving(IsPlayerMoving());

        SetLastPosition();
        Move(position);
        HandleFlip(position);
    }

    private void Move(Vector2 direction)
    {
        rigidBody.MovePosition(rigidBody.position + moveSpeed * Time.fixedDeltaTime * direction);
    }

    private void SetLastPosition()
    {
        if (IsPlayerMoving())
        {
            lastPosition = position;
        }
    }

    private bool IsPlayerMoving()
    {
        return position != Vector2.zero;
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

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaler = childToFlip.localScale;
        scaler.x *= -1;
        childToFlip.localScale = scaler;
    }

    private void SetRigidbody2DSettings()
    {
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidBody.gravityScale = 0;
        rigidBody.angularDrag = 0;
    }

    public void MonsterAttack()
    {
        StartCoroutine(animHandler.SetAttackTrigger());
    }
}
