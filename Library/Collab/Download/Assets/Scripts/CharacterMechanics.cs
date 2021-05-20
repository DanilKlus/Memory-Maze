using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public float movementSpeed;
    public float jumpPower;
    public float rotationAngle;

    private Vector3 movementVector;

    private CharacterController ch_controller;
    private Animator ch_animator;

    // Start is called before the first frame update
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis("Horizontal") * movementSpeed;
        movementVector.z = Input.GetAxis("Vertical") * movementSpeed;
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, rotationAngle);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -rotationAngle);
        if (Input.GetKey(KeyCode.W))
        {
            movementVector = transform.TransformDirection(movementVector);
            ch_controller.Move(movementVector * Time.deltaTime);
        }
    }
}
