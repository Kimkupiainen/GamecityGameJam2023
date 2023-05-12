using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("0 for p1 1 for p2"),Range(0,1)]
    int playernumber = 0;
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private CapsuleCollider2D boxCollider;

    private Vector2 velocity;

    //public InputAction playerControls;
    public PlayerControls playerControls;
    private InputAction move;
    private InputAction move2;

    private InputAction jump;
 
    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;

    private void Awake()
    {
        boxCollider = GetComponent<CapsuleCollider2D>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move2 = playerControls.Player.Move2;
        move.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        move.Disable();
    }

    private void Update()
    {
        if (playernumber == 0)
        {
            // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
            //float moveInput = Input.GetAxisRaw("Horizontal1");

            float moveInput = move.ReadValue<Vector2>().x;

            if (grounded)
            {
                velocity.y = 0;

                if (Input.GetButtonDown("Jump1"))
                {
                    // Calculate the velocity required to achieve the target jump height.
                    velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                }
            }

            float acceleration = grounded ? walkAcceleration : airAcceleration;
            float deceleration = grounded ? groundDeceleration : 0;

            if (moveInput != 0)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            }

            velocity.y += Physics2D.gravity.y * Time.deltaTime;

            //transform.Translate(velocity * Time.deltaTime);

            grounded = false;
        }
        if (playernumber == 1)
        {
            // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
            //float moveInput = Input.GetAxisRaw("Horizontal2");
            float moveInput = move2.ReadValue<Vector2>().x;

            if (grounded)
            {
                velocity.y = 0;

                if (Input.GetButtonDown("Jump2"))
                {
                    // Calculate the velocity required to achieve the target jump height.
                    velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                }
            }

            float acceleration = grounded ? walkAcceleration : airAcceleration;
            float deceleration = grounded ? groundDeceleration : 0;

            if (moveInput != 0)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            }
            

            velocity.y += Physics2D.gravity.y * Time.deltaTime;

            //transform.Translate(velocity * Time.deltaTime);

            grounded = false;
        }


        // Retrieve all colliders we have intersected after velocity has been applied.
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                // If we intersect an object beneath us, set grounded to true. 
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}