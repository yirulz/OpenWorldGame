using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 8f;
    public float groundRayDistance = 1.1f;

    //Reference to character controller
    private CharacterController controller;
    private Vector3 motion; //Is the movement offset per frame

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Move(inputH, inputV);

        //If character is grounded
        if(IsGrounded())
        {
            motion.y = 0f;
            //If you press the jump key
            if(Input.GetButtonDown("Jump"))
            {
                //Players y will equal jump height
                motion.y = jumpHeight;
            }
        }

        motion.y += gravity * Time.deltaTime;

        controller.Move(motion * Time.deltaTime);
    }

    bool IsGrounded()
    {
        //Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        //If hitting something
        return (Physics.Raycast(groundRay, groundRayDistance));
        
    }

    void Move(float inputH, float inputV)
    {
        //Generate direction form input
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        //Convery local space to world space direction
        direction = transform.TransformDirection(direction);

        //Apply motion
        motion.x = direction.x * walkSpeed;
        motion.z = direction.z * walkSpeed;
    }
}
