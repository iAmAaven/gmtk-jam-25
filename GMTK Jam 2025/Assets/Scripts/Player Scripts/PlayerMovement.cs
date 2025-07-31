using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Publics
    [Header("Flight")]
    public float flightSpeed = 10f;
    public float torqueAmount = 0.025f, maxTorqueSpeed = 100f, autoTorque = 0.25f;

    [Header("Grounded Movement")]
    public float moveSpeed;
    public float groundedWidth, groundedHeight;


    [Header("References")]
    public Transform feetPos;
    public Animator animator;
    public LayerMask groundLayer;



    // Privates
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(feetPos.position, new Vector2(groundedWidth, groundedHeight), 0, groundLayer);


        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", horizontalInput);

        if (isGrounded)
        {
            rb.gravityScale = 1;
            rb.freezeRotation = true;
            transform.localEulerAngles = Vector3.zero;
            if (horizontalInput > 0)
                rb.linearVelocityX = horizontalInput * moveSpeed;
            else
                rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0, moveSpeed * Time.deltaTime);

            return;
        }
        else
        {
            rb.freezeRotation = false;
            rb.gravityScale = 0.1f;
            // The plane goes right all the time and can be moved up or down using the horizontal input
            rb.linearVelocity = rb.transform.right * flightSpeed;
            rb.AddTorque(-horizontalInput * torqueAmount, ForceMode2D.Impulse);

            float zRotation = transform.localEulerAngles.z;
            if ((zRotation <= 60f && zRotation >= 0f) || (zRotation <= 360f && zRotation >= 300f))
                rb.AddTorque(-autoTorque, ForceMode2D.Force);
            else if (zRotation >= 120f && zRotation <= 240f)
                rb.AddTorque(autoTorque, ForceMode2D.Force);

            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxTorqueSpeed, maxTorqueSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(feetPos.position, new Vector2(groundedWidth, groundedHeight));
    }
}
