using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpHeight = 1f;
    Rigidbody2D playerRb;
    Vector2 moveInput;

    void Start() {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if (value.isPressed) {
            playerRb.velocity += new Vector2(0f, jumpHeight);
        }
    }

    private void Run() {
        playerRb.velocity = new Vector2(moveInput.x * speed, playerRb.velocity.y);
    }

    void FlipSprite() {
        bool horizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;
        if (horizontalMovement) {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }
    }
}
