using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;
    [SerializeField] private Animator animator;
    
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerController.OnMove += Move;
    }

    private void Move(Vector2 movement)
    {
        rb.velocity = movement * speed * Time.fixedDeltaTime;
        animator.SetInteger("HorizontalVel", (int) movement.x);
    }
}
