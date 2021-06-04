using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public float MovementSpeed = 3f;
    public float SpeedMultiplier = 2f;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private PlayerControl control;

    private void Start()
    {
        control = GetComponent<PlayerControl>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        if (control.ElapsedTime == 0)
        {
            move *= SpeedMultiplier;
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
        }

        velocity.y += Gravity * Time.deltaTime;

        Controller.Move(move * MovementSpeed * Time.deltaTime);
        Controller.Move(velocity * Time.deltaTime);
    }
}
