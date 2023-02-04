using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, ReadOnly]
    Interactable target = null;

    [Space]

    [SerializeField]
    float moveSpeed = 10;
    [SerializeField, ReadOnly]
    Vector3 moveDirection;
    [SerializeField, ReadOnly]
    Vector3 lookDirection;
    Rigidbody rb;
    //PlayerController controller;

    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float playerSpeed = 2.0f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;

    Transform orientation;


    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        ////Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        ////transform.Rotate(lookDirection * Time.deltaTime * playerSpeed);
        //controller.Move(moveDirection * Time.deltaTime * playerSpeed);

        ////if (lookDirection != Vector3.zero)
        ////    gameObject.transform.forward = lookDirection;

        //// Changes the height position of the player..
        ////if (Input.GetButtonDown("Jump") && groundedPlayer)
        //if (Keyboard.current.spaceKey.isPressed && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);

        //MovePlayer();
        //RotatePlayer();
        //ShootRaycast();
    }

    private void RotatePlayer()
    {
        Vector3 lookRotation = new Vector2(lookDirection.x, lookDirection.y);
        lookRotation.x = Math.Clamp(lookRotation.x, -90f, 90f);
        transform.rotation = Quaternion.Euler(lookRotation);
        orientation.rotation = Quaternion.Euler(0, lookRotation.y, 0);
    }

    //void MovePlayer()
    //{
    //    //rb.MovePosition(transform.position + moveSpeed * Time.deltaTime * moveDirection);
    //    controller.moveDirection = moveSpeed * Time.deltaTime;
    //    controller.moveDirection = moveDirection;
    //}

    void ShootRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Ray ray = Camera.main.ViewportPointToRay(Camera.main.transform.forward);
        Physics.Raycast(ray, out RaycastHit hitData);
        Debug.DrawRay(ray.origin, ray.direction);

        if (hitData.collider && hitData.collider.CompareTag("Interactable"))
            target = hitData.collider.GetComponent<Interactable>();
        else
            target = null;
    }

    #region Input methods

    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        moveDirection = new Vector3(dir.x, 0, dir.y);
    }

    private void OnLook(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        lookDirection = new Vector3(0,dir.x,0);

        //transform.Rotate(lookDirection);
    }

    private void OnFire()
    {
        Debug.Log("OnFire triggered!");
        if (target != null)
            target.TriggerInteraction();
        //if ()
    }

    #endregion
}
