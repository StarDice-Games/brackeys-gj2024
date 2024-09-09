using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class Player : MonoBehaviour
{
    [SerializeField] InputController inputController;

    [SerializeField, Range(0, 25)] float moveSpeed = 5f;
    Vector2 position;
    bool isFacingRight = true;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        position.x = inputController.MovementValue.x;
        position.y = inputController.MovementValue.y;

        Move(position);
        HandleFlip(position);
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
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
