using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float movepeedcontrol;
    public bool canSprint;
    public float runTime; 
    public float runMultiply;
    public bool sprinting;

    [Header("Keybinds")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode crouchkey = KeyCode.LeftControl;
    public KeyCode dashkey = KeyCode.LeftShift;

    [Header("Ground check")]
    public float PlayerHeight;
    public LayerMask whatisgrnd;

    public Animator animator;
    public AudioSource Footsteps;

    [Header("crouching")]
    public float crouchspeed;
    public float crouchyscale;
    private float startyscle;
    public Transform arm1;
    public bool iscrouching;

    [Header("Dashing")]
    public float dashforce;
    public float dashcooldown;
    public bool readyDash;



    public float grounddrag;
    public bool grounded;

    public float jumpForce;
    public float JumpCooldown;
    public float airmulti;
    public bool readyjump;

    public Transform orient;

    float horizantalinput;
    float verticalinput;


    Vector3 movedir;

    Rigidbody rb;
    public movementstate state;
    public enum movementstate
    {
        walking,
        air,
        crouching,

    }


    private void StateHandler()
    {
        if (Input.GetKey(crouchkey)){

            state = movementstate.crouching;
            moveSpeed = crouchspeed;
        }
        

       else if (grounded)
        {
            state = movementstate.walking;
            animator.SetBool("jump", false);
        }
        else
        {
            state = movementstate.air;
            animator.SetBool("jump", true);
        }





    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyjump = true;

        startyscle = transform.localScale.y;
    }
    void Update()
    {
        
        Speedcontrol();
        myinput();
        StateHandler();
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatisgrnd);
        
        if (grounded)
        {
            rb.linearDamping = grounddrag;

        }
        else
            rb.linearDamping = 0;
    }
    void FixedUpdate()
    {
        MovePlayer();

    }

    void myinput()
    {
        horizantalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpkey) && readyjump && grounded)
        {
            readyjump = false;

            Jump();

            Invoke(nameof(resetjump), JumpCooldown);
        }

        if (Input.GetKeyDown(crouchkey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchyscale, transform.localScale.z);
            arm1.transform.localScale = new Vector3(transform.localScale.x, 2.0f, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchkey))
        {
            arm1.transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            transform.localScale = new Vector3(transform.localScale.x, startyscle, transform.localScale.z);
            moveSpeed = movepeedcontrol; 
        }
        if (Input.GetKeyDown(dashkey) && readyDash == true)
        {
            readyDash = false;
            Vector3 dashdir = orient.forward * verticalinput + orient.right * horizantalinput;
            animator.SetTrigger("Dash");
            if (dashdir == Vector3.zero)
                dashdir = orient.forward;

            rb.AddForce(dashdir.normalized * dashforce, ForceMode.Impulse);
            Invoke(nameof(resetDash), dashcooldown);


        }
    }
    
    void MovePlayer()
    {
        movedir = orient.forward * verticalinput + orient.right * horizantalinput;
        rb.AddForce(movedir.normalized * moveSpeed * 10f, ForceMode.Force);
        // Animation check
        if (movedir.magnitude > 0.1f)
        { // small threshold so tiny input doesn’t trigger running
            animator.SetBool("Running", true);
            if (grounded)
                Footsteps.enabled = true;
        }
        else
        {
            if (sprinting)
            {
                canSprint = true;
                StopSprinting();
            }
            animator.SetBool("Running", false);
            Footsteps.enabled = false;
        }
        if (grounded)
        {
            rb.AddForce(movedir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
            rb.AddForce(movedir.normalized * moveSpeed * 10f * airmulti, ForceMode.Force);

        if (canSprint && sprinting == false)
        {
            canSprint = false;
            Invoke(nameof(startSprinting), runTime);

        }

    }


    void Speedcontrol()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("jump", true);
    }

    private void resetjump()
    {
        readyjump = true;
    }
    private void resetDash()
    {
        readyDash = true;
    }
    private void startSprinting()
    {
        canSprint = false;
        sprinting = true;
        moveSpeed = moveSpeed * runMultiply;
        movepeedcontrol = moveSpeed;
    }
    private void StopSprinting()
    {
        canSprint = true;
        sprinting = false;
        moveSpeed = moveSpeed / runMultiply;
        movepeedcontrol = moveSpeed;
    }
}
