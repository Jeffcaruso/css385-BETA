using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class josephPlayeBehavior : MonoBehaviour
{
    public float startSpeed = 1f;
    public float maxSpeed = 10f;
    public float timeToMaxSpeed = 1.0f;
    public float maxJumpForce = 4f;
    public float baseJumpForce = 1f;
    public float timeToMaxJumpForce = 0.2f;
    public float hitDelay = 2.0f;

    private float currentSpeed;
    private float jumpForce;
    private bool isJumping;
    private bool isGrounded;
    private float jumpKeyHoldTime;
    private Vector2 storedVelocity;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = startSpeed;
        jumpKeyHoldTime = 0;
        currentSpeed = startSpeed;
        
    }
    private void OnDisable() {
        storedVelocity = new Vector2(rb.velocity.x, 0);
    }

    private void OnEnable() {
        rb.velocity = rb.velocity + storedVelocity;
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.S))
            {
                // Key hold time between 0 and timeToMaxJumpForce
                jumpKeyHoldTime = Mathf.Clamp(jumpKeyHoldTime + Time.deltaTime, 0, timeToMaxJumpForce);
                // Calculate jump force
                jumpForce = Mathf.Lerp(baseJumpForce, maxJumpForce, jumpKeyHoldTime / timeToMaxJumpForce);
                Debug.Log("Jump Key Held for: " + jumpKeyHoldTime + " seconds. Jump force is now: " + jumpForce);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isJumping = true;
                jumpKeyHoldTime = 0; // Reset key hold time
                Debug.Log("Jump key released. Jumping with force: " + jumpForce);
            }

            if(rb.velocity.y < 0)
            {
                rb.gravityScale = rb.gravityScale * 2;
            } else {
                rb.gravityScale = 50;
            }
        }

        // Calculate the total amount of speed to gain
        float totalSpeedToGain = maxSpeed - startSpeed;

        // Calculate how much speed to gain each second
        float speedToGainPerSecond = totalSpeedToGain / timeToMaxSpeed;

        // Increase speed over time until it reaches maxSpeed
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, speedToGainPerSecond * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Apply horizontal speed
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // Apply jump force
        if (isJumping && isGrounded)
        {
            // Set vertical velocity directly
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Triggered with Obstacle");
            StartCoroutine(ImmobilizePlayer(hitDelay)); // Immobilize for 2 seconds
            StartCoroutine(FlashSprite());
        }
    }

    IEnumerator ImmobilizePlayer(float delay)
    {
        currentSpeed = 1f; // Immobilize player
        yield return new WaitForSeconds(delay); // Wait for the delay
        currentSpeed = startSpeed; // Set speed back to startSpeed
    }

    IEnumerator FlashSprite()
    {
        float endTime = Time.time + 0.5f;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return null;
        }
        spriteRenderer.enabled = true; // Make sure sprite is visible at the end
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if player is on the ground
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if player has left the ground
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }


}
