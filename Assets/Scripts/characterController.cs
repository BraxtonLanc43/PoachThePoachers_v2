using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class characterController : MonoBehaviour
{
    //Most pieces used from Brackey's script here: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
    [SerializeField] private float jumpForce = 1000f; // Amount of force added when the player jumps.
    [SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded; // Whether or not the player is grounded.
    private Rigidbody2D myRigidbody2D;
    private bool isFacingRight = true; // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    bool wasGrounded = true;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // Start is called before the first frame update
    void Start()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.gravityScale = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float move, bool jump)
    {
        // If the player should jump...
        if (isGrounded && jump)
        {
            float jumpForceToApply = jumpForce * 2.0f;
            //Debug.Log("Jump Force: " + jumpForceToApply);
            // Add a vertical force to the player.
            myRigidbody2D.AddForce(new Vector2(0f, 1100f));
            isGrounded = false;
        }
        else
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 3.5f, myRigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    private void FixedUpdate()
    {
        wasGrounded = isGrounded;
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            //Debug.Log("OnCollisionEnter2D");
            isGrounded = true;
            if (!wasGrounded)
            {
                OnLandEvent.Invoke();
                wasGrounded = false;
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    
}