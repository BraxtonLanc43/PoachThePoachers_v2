using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class hunterController : MonoBehaviour
{
    //Most pieces used from Brackey's script here: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
    [SerializeField] private float jumpForce = 1000f; // Amount of force added when the player jumps.
    [SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
    private bool isGrounded; // Whether or not the player is grounded.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement
    private Rigidbody2D myRigidbody2D;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        wasGrounded = isGrounded;
    }

    public void Move(float move, bool jump)
    {
        // If the player should jump...
        if (isGrounded && jump)
        {
            myRigidbody2D.AddForce(new Vector2(0f, jumpForce * 0.75f));
            isGrounded = false;
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                OnLandEvent.Invoke();
                wasGrounded = false;
            }
        }
    }
    
}
