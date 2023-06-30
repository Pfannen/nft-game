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
    [SerializeField] Transform mountSpawn;
    //Temporary field
    [SerializeField] bool toggleMount = false;

    EquipmentManager manager;
    Rigidbody2D playerRb;
    BoxCollider2D playerCollider;
    Vector2 moveInput;
    float initialPlayerGravity;

    Mount mount;

    void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        manager = GetComponent<EquipmentManager>();
    }

    void Start() {
        initialPlayerGravity = playerRb.gravityScale;
        if (manager && toggleMount) OnEquipmentChange();
    }

    void Update() {
        if (mount) {
            MoveMount();
        } else {
            Run();
            Climb();
        }
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

    private void MoveMount() {
        playerRb.velocity = new Vector2(moveInput.x * mount.MountSpeed, moveInput.y * mount.MountSpeed);
    }

    private void FlipSprite() {
        bool horizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;
        if (horizontalMovement) {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }
    }

    private void OnEquipmentChange() {
        EquipMount(manager.GetEquipment(EquipmentType.Mount) as Mount);
    }

    private void EquipMount(Mount mount) {
        this.mount = mount;
        for (int i = mountSpawn.childCount - 1; i >= 0; i--) {
            Destroy(mountSpawn.GetChild(i).gameObject);
        }
        if (!mount) playerRb.gravityScale = initialPlayerGravity;
        else {
            if (mount.Prefab != null) Instantiate(mount.Prefab, mountSpawn.position, Quaternion.identity, mountSpawn);
            playerRb.gravityScale = 0;
        } 
    }

    void OnEnable() {
        if (manager) manager.EquipmentChanged += OnEquipmentChange;
    }

    void OnDisable() {
        if (manager) manager.EquipmentChanged -= OnEquipmentChange;
    }
}
