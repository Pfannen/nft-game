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

    private void Jump() {
        playerRb.velocity += new Vector2(0f, moveInput.y * jumpHeight);
    }
}
