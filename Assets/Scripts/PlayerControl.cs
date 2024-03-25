using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Callandra Ruiter GDD316 2024
// This code controls the player snowmobile with WASD keys (and also the UI if gameplay is enabled)

//code referenced (with alterations) from tutorial: https://www.youtube.com/watch?v=f473C43s8nE
public class PlayerControl : MonoBehaviour
{

    [SerializeField] int playerSpeed;
    [SerializeField] int playerJumpHeight;
    [SerializeField] bool gamePlay;

    //private Animator anim;
    private Rigidbody rb;

    public bool grounded;

    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    Vector3 moveDirection;

    public int bumps;
    [SerializeField] GameObject uiDisplay;
    [SerializeField] TextMeshProUGUI bumpDisplay;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (gamePlay) //if gameplay enabled, sets the display active
        {
            uiDisplay.SetActive(true);
        }
        else //otherwise unneeded, set inactive
        {
            uiDisplay.SetActive(false);
        }

    }

    private void Update()
    {
        //ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        //check for player input, control speed
        MyInput();
        SpeedControl();

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;



        Quaternion deltaRotation = Quaternion.Euler(0,horizontalInput * (playerSpeed/2) * Time.fixedDeltaTime,0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // on ground
        if (grounded) { 
        rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
        }

        // in air
        else if (!grounded)
        {
            //rb.AddForce(moveDirection.normalized * playerSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
        }

    }

    //make sure player isn't going too fast
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }


    //manage groundcheck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            bumps++;
            UpdateDisplay();
        }
    }

    //manage groundcheck
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other) // if the player enters a shelter or falls off the map
    {
        if (other.gameObject.CompareTag("Shelter")) // counter gets reset
        {
            bumps = 0;
            UpdateDisplay(); 
        } else if (other.gameObject.CompareTag("Teleport")) // player teleports back up onto map
        {
            gameObject.transform.position = new Vector3(7, 5, 5);
        }
    }


    /**
    private void BeginDisplay()
    {
        uiDisplay.SetActive(true);
    }**/

    private void UpdateDisplay() //updates the UI display
    {
        if (gamePlay)
        {
            bumpDisplay.text = "Crashes since last shelter: " + bumps;
        }
    }

    // Start is called before the first frame update
    /**void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 editing = new Vector3(0, 0, 0);
        //basic movement + rotate?
        if (Input.GetKey(KeyCode.W))
        {
            //forward
            if (rb.velocity.z != playerSpeed)
            {
                editing += new Vector3(0, 0, playerSpeed);
                //Debug.Log("forward?");
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            //back
            if (rb.velocity.z != -playerSpeed)
            {
                editing += new Vector3(0, 0, -playerSpeed);
                //Debug.Log("back?");
            }

        }
        else
        {
            //slow down
            rb.velocity = transform.TransformDirection(new Vector3(rb.velocity.x, rb.velocity.y, 0));
        }

        //separate so both can happen at same time
        if (Input.GetKey(KeyCode.D))
        {
            //right
            if (rb.velocity.x != playerSpeed)
            {
                transform.Rotate(Vector3.up * playerSpeed * Time.deltaTime);
                //editing += new Vector3(playerSpeed, 0, 0);
                //Debug.Log("right?");
            }

        }
        else if (Input.GetKey(KeyCode.A))
        {
            //left
            if (rb.velocity.x != -playerSpeed)
            {
                transform.Rotate(-Vector3.up * playerSpeed * Time.deltaTime);
                //editing -= new Vector3(playerSpeed, 0, 0);
                // Debug.Log("left?");
            }
        }
        else
        {
            //slow down
            rb.velocity = transform.TransformDirection(new Vector3(0, rb.velocity.y, rb.velocity.z));

        }

        rb.velocity += transform.TransformDirection(editing);
        //transform.rotation = Quaternion.Slerp(0, transform.rotation.y, transform.rotation.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
            rb.AddForce(0, playerJumpHeight, 0);
        }


    }**/

}
