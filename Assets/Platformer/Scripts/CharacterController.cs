using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    private Animator animator;

    public float acceleration = 30f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 10f;
    public float jumpBoostForce = 12f;

    private bool inAir;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing from " + gameObject.name);
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from " + gameObject.name);
        }
    }

    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");

        if (rb != null)
        {
            rb.linearVelocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;

            // clamp horizontal speed
            float horizontalSpeed = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
            rb.linearVelocity = new Vector3(horizontalSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalAmount));
        }

        //check if touching ground
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            float castDistance = collider.bounds.extents.y + 0.05f;
            Vector3 startPoint = transform.position;
            isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

            Color color = (isGrounded) ? Color.green : Color.red;
            Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            inAir = true;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpImpulse, rb.linearVelocity.z);

            if (animator != null)
            {
                animator.SetBool("IsJumping", true);
            }
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                inAir = true;
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);

                if (animator != null)
                {
                    animator.SetBool("IsJumping", true);
                }
            }
        }

        // reset animation when grounded
        if (isGrounded && inAir)
        {
            inAir = false;
            if (animator != null)
            {
                animator.SetBool("IsJumping", false);
            }
        }

        // smooth decel when stopping
        if (horizontalAmount == 0 && rb != null)
        {
            Vector3 decayVelocity = rb.linearVelocity;
            decayVelocity.x *= 1f - Time.deltaTime * 4f; //friction effect
            rb.linearVelocity = decayVelocity;
        }
        else
        {
            //rotate character in moving direction
            float yawRotation = (horizontalAmount > 0) ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0f, yawRotation, 0f);
        }
    }
}
