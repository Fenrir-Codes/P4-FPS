using UnityEngine;

public class Movement : MonoBehaviour
{
    // This script have to be attached on tha player object.

    //character controller
    [SerializeField]
    private CharacterController controller;
    // private gun gunAnimation;
    private Animator anim;


    //Variables
    [SerializeField]
    private float Gravity = 9.81f;
    private float moveSpeed = 0f;
    [SerializeField]
    private float walkSpeed = 8f;
    [SerializeField]
    private float runSpeed = 12f;
    [SerializeField]
    private float jumpForce = 3f;

    //Vectors
    private Vector3 moveDirection;
    private Vector3 Velocity;

    //Booleans
    private bool isGrounded;

    //Keycodes
    private KeyCode jumpButton = KeyCode.Space;
    private KeyCode runButton = KeyCode.LeftShift;

    // private void Awake()
    // {
    //     gunAnimation = new gun();
    // }

    // Update is called once per frame
    void Update()
    {
        // anim = gunAnimation.anim = GetComponentInChildren<Animator>();
        isGrounded = controller.isGrounded;
        Movements();
    }

    #region Movement funktion
    void Movements()
    {
        //Resetting velocity -->
        if (isGrounded && Velocity.y <= 0)
        {
            Velocity.y = -2f;
            //Debug.Log(Velocity.y);
        }

        //Getting axes -->
        moveDirection.Normalize();
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        //Move the player -->
        if (moveDirection != Vector3.zero)
        {
            moveSpeed = walkSpeed;
            moveDirection = transform.right * moveDirection.x + transform.forward * moveDirection.y;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);


        }
        //Run -->
        if (Input.GetKey(runButton))
        {
            Run();
        }
        //Jumping -->
        if (Input.GetKey(jumpButton) && isGrounded)
        {
            Jump();
        }

        //apply gravity -->
        Velocity.y -= Gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        //Debug.Log("movespeed:" + moveSpeed);
    }
    #endregion

    #region Jump Function
    void Jump()
    {
        Velocity.y = jumpForce;
    }
    #endregion

    #region Run function
    void Run()
    {
        controller.Move(moveDirection * runSpeed * Time.deltaTime);
    }
    #endregion

}
