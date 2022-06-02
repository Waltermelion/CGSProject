using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float sprint = 7f;
    public float gravity = -9.0f;
    public float jumpHeight = 3f;



    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        

    }

    //receive the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;


        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y =  -2f;
        controller.Move(playerVelocity * Time.deltaTime);

        if(isGrounded && inputManager.onFoot.Run.IsPressed())
        {
            controller.Move(transform.TransformDirection(moveDirection) * speed * 100 *Time.deltaTime);
        }
        
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
