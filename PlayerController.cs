using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 9.81f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isRunning;

    private Animator animator;
    [SerializeField] private GameObject body;
    [SerializeField] private float velocityZ = 0.0f;
    [SerializeField] private float velocityX = 0.0f;

    public AudioSource walkingSound;
    public AudioClip walk;
    public AudioClip run;

    [SerializeField] private Vector2 moveVal;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        walkingSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isRunning)
        {
            moveSpeed = 6f;
        }
        else
        {
            moveSpeed = 4f;
        }

        if (moveVal.x != 0 || moveVal.y != 0)
        {
            if (moveSpeed == 4f)
            {
                playWalkingSound();
            }
            if (moveSpeed == 6f)
            {
                playRunningSound();
            }
        }
        else
        {
            walkingSound.Stop();
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        Vector3 moveDir = new Vector3(moveVal.x, 0, moveVal.y);

        Vector3 movement = moveDir * moveSpeed * Time.deltaTime;

        // player animation match player direction
        Vector3 animationVector = body.transform.InverseTransformDirection(moveDir);
        
        // player running and walk animation
        if (isRunning)
        {
            velocityX = animationVector.x * 2;
            velocityZ = animationVector.z * 2;
        }
        else
        {
            velocityX = animationVector.x;
            velocityZ = animationVector.z;
        }

        // Move the player
        if (isGrounded)
        {
            rb.MovePosition(transform.position + movement);

        }
        else if (isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity * rb.mass);
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }

    // input action
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVal = context.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Right mouse button pressed");
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    // audio
    public void playWalkingSound()
    {
        float rand = Random.Range(-0.1f, 0.1f);

        walkingSound.pitch = 1 + rand;

        if (!walkingSound.isPlaying)
        {
            walkingSound.clip = walk;
            walkingSound.Play();
        }
    }
    public void playRunningSound()
    {
        float rand = Random.Range(-0.1f, 0.1f);

        walkingSound.pitch = 0.9f + rand;

        if (!walkingSound.isPlaying)
        {
            walkingSound.clip = run;
            walkingSound.Play();
        }
    }
}
