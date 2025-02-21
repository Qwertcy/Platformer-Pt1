using UnityEngine;

public class CharacterController : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    public float acceleration = 30f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;

    [Header("Debug stuff")]
    public bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;
        
        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;

        rb.linearVelocity = newVelocity;

        //should also clamp vertical velocity

        //test if character is touching the ground
        Collider collider = GetComponent<Collider>();
        
        float castDistance = collider.bounds.extents.y + 0.05f;
        Vector3 startPoint = transform.position;
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //apply an impulse force upward
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if(rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
        }

        if (horizontalAmount == 0)
        {
            Vector3 decayVelocity = rb.linearVelocity;
            decayVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayVelocity;
        }
        else
        {
            float yawRotation = (horizontalAmount > 0) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }
    }
}
