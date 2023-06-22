using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementPattern { Circle, ToPlayer }

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("Circle Movement")]
    [SerializeField] private MovementPattern movementPattern;
    [SerializeField] private float circleRadius = 3f;
    [SerializeField] private float circleSpeed = 2f;
    [SerializeField] private float downwardSpeed = 500f;
    
    private Vector3 startPosition;
    private float startTime;

    [Header("To Player Movement")] 
    [SerializeField] private float movementSpeed = 1f;
    private bool triggerTargetPosition;
    private Vector3 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startTime = Time.time;
        triggerTargetPosition = true;
    }

    private void Update()
    {
        switch (movementPattern)
        {
            case MovementPattern.Circle:
                CircleMovement();
                break;
            case MovementPattern.ToPlayer:
                ToPlayerMovement();
                break;
        }
    }

    private void CircleMovement()
    {
        float elapsedTime = Time.time - startTime;
        float angle = elapsedTime * circleSpeed;
        float x = Mathf.Cos(angle) * circleRadius;
        float y = Mathf.Sin(angle) * circleRadius;

        Vector2 circlePosition = startPosition + new Vector3(x, y);
        rb.MovePosition(circlePosition + Vector2.down * elapsedTime * downwardSpeed);
    }

    private void ToPlayerMovement()
    {
        if (triggerTargetPosition)
        {
            targetPosition = PlayerController.Instance.transform.position;
            triggerTargetPosition = false;
        }
        Vector2 direction = (targetPosition - transform.position).normalized;
        Vector2 movement = direction * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        if (Vector3.Distance(transform.position, targetPosition) <= 0.5f) triggerTargetPosition = true;
    }
    
    public void ResetValues(Vector3 newPosition, MovementPattern pattern)
    {
        startPosition = newPosition;
        transform.position = newPosition;
        startTime = Time.time;
        triggerTargetPosition = true;
        movementPattern = pattern;
    }
}
