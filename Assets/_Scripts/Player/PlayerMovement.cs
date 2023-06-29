using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] LayerMask ground;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] LayerMask climbing;

    Rigidbody2D playerRb;
    BoxCollider2D playerCollider;
    Vector2 moveInput;
    float initialPlayerGravity;

    void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        initialPlayerGravity = playerRb.gravityScale;
    }

    void Update() {
        Run();
        Climb();
        FlipSprite();
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if (value.isPressed && IsTouchingGround()) {
            playerRb.velocity += new Vector2(0f, jumpHeight);
        }
    }

    private bool IsTouchingGround()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, ground).collider != null;
    }

    private void Run() {
        playerRb.velocity = new Vector2(moveInput.x * speed, playerRb.velocity.y);
    }

    private void Climb() {
        if (playerCollider.IsTouchingLayers(climbing)) {
            playerRb.gravityScale = 0;
            /* if (Math.Abs(moveInput.y) > Mathf.Epsilon)  */playerRb.velocity = new Vector2(playerRb.velocity.x, moveInput.y * climbSpeed);
        } else playerRb.gravityScale = initialPlayerGravity;
    }

    void FlipSprite() {
        bool horizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;
        if (horizontalMovement) {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }
    }
}
