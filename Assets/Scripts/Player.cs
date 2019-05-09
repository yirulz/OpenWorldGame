using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;

    [Header("Dash")]
    public float dashSpeed = 50f;
    public float dashDuration = .5f;

    private CharacterController controller; // Reference to CharacterController
    private Vector3 motion; // Is the movement offset per frame
    private bool isJumping;
    private float currentJumpHeight, currentSpeed;

    void OnValidate()
    {
        currentJumpHeight = jumpHeight;
        currentSpeed = walkSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        currentJumpHeight = jumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // Get W, A, S, D or Left, Right, Up, Down Input
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
        bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);

        //Space bar input
        bool inputJump = Input.GetButtonDown("Jump");

        //change current speed to runspeed
        if(inputRun)
        {
            currentSpeed = runSpeed;
        }
        //change currentspeed to walkspeed
        if(inputWalk)
        {
            currentSpeed = walkSpeed;
        }
        // Move character motion with inputs
        Move(inputH, inputV);

        // Is the Player grounded?
        if (controller.isGrounded)
        {
            // Cancel gravity
            motion.y = 0f;
            // Pressing Jump Button?
            if (inputJump)
            {
                Jump(jumpHeight);
            }
            if(isJumping)
            {
                // Make the Player Jump!
                motion.y = currentJumpHeight;
                //Reset back to false
                isJumping = false;
            }
        }
        // Apply gravity
        motion.y += gravity * Time.deltaTime;
        // Move the controller with motion
        controller.Move(motion * Time.deltaTime);
    }

    // Move the character's motion in direction of input
    void Move(float inputH, float inputV)
    {
        // Generate direction from input
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        // Convert local space to world space direction
        direction = transform.TransformDirection(direction);

        //Check if direction exceeds magnitude of 1
        if(direction.magnitude > 1f)
        {
            //Normalize it
            direction.Normalize();
        }

        // Apply motion to only X and Z
        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }


    public void Jump(float height)
    {
        isJumping = true; //Tell controller to jump at the right time
        currentJumpHeight = height;
    }

    public IEnumerator SpeedBoost(float startSpeed, float endSpeed, float duration)
    {
        currentSpeed = startSpeed;

        yield return new WaitForSeconds(duration);

        currentSpeed = endSpeed;
    }

    public void Dash()
    {
        StartCoroutine(SpeedBoost(dashSpeed, walkSpeed, dashDuration));
    }

}
