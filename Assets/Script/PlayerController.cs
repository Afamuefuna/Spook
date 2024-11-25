using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isGrounded = true;
    public bool hasPlayedGroundEffect;

    public Transform groundCheck; // A point near the player's feet (can be set in the inspector)
    public float checkRadius = 0.2f; // Radius for ground check
    public LayerMask groundLayer; // Layer for the ground
    public float movementDirection;
    public Rigidbody2D rb;
    public float jumpForce;
    public ParticleSystem movementParticle, groundParticle;
    public Vector2 restartPoint;
    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector3.right * Time.deltaTime * speed * (horizontalInput + movementDirection));
        if(horizontalInput != 0){
            movementParticle.Play();
        }

        CheckIfGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    public void MoveLeft()
    {
        movementDirection = -1;
    }

    public void MoveRight()
    {
        movementDirection = 1;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void CheckIfGrounded()
    {
        // Perform a raycast downwards from the groundCheck point
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkRadius, groundLayer);

        // Check if the raycast hit the ground
        isGrounded = hit.collider != null;

        if(isGrounded){
            if(!hasPlayedGroundEffect){
                groundParticle.Play();
                AudioManager.Instance.PlaySound(SoundEffect.LAND);
                hasPlayedGroundEffect = true;
            }
        }else{
            hasPlayedGroundEffect = false;
        }
    }
}
