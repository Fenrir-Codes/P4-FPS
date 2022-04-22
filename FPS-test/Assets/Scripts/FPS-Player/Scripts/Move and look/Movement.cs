using UnityEngine;

public class Movement : MonoBehaviour
{
    // This script have to be attached on tha player object.

    //character controller
    [Header("Character controller (First person player)")]
    [Tooltip("The caharacter controller")]
    [SerializeField] private CharacterController controller;
    [Tooltip("Animator for the character body")]
    [SerializeField] private Animator animator;

    private Animator anim;  // animator on the weapon

    //Variables
    [Header("Gravity setting")]
    [Tooltip("Gravity value")]
    [SerializeField] private float Gravity = 9.81f;

    [Header("Player settings")]
    [Tooltip("Walking speed value")]
    [SerializeField] private float walkSpeed = 8f;
    [Tooltip("Running speed value")]
    [SerializeField]  private float runSpeed = 12f;
    [Tooltip("Jump force value")]
    [SerializeField]  private float jumpForce = 3f;

    private float moveSpeed = 0f;

    //Vectors
    private Vector3 moveDirection;
    private Vector3 Velocity;

    //Booleans
    private bool isGrounded;
    public bool isWalking;

    //Keycodes
    private KeyCode jumpButton = KeyCode.Space;
    private KeyCode runButton = KeyCode.LeftShift;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();  // getting weapon animator
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        Movements();
    }

    #region Movement funktion
    void Movements() 
    {
        if (isGrounded && moveDirection != Vector3.zero)
        {
            anim.SetBool("walking", true);
            isWalking = true;
        }
        else
        {
            isWalking = false;
            anim.SetBool("walking", false);
        }

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
            movePlayer();      
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

    #region move the character
    void movePlayer()
    {
        moveSpeed = walkSpeed;
        moveDirection = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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
