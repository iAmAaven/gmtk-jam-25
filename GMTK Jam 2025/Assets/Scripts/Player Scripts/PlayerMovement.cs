using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Flight")]
    public float flightSpeed = 10f;
    public float torqueAmount = 0.025f, maxTorqueSpeed = 100f, autoTorque = 0.25f;

    [Header("Landed Movement")]
    public float landSpeed;
    public float landingWidth, landingHeight;
    public float coyoteTime = 0.1f;

    [Header("References")]
    public Transform feetPos;
    public Animator animator;
    public LayerMask groundLayer;
    public bool debuggingMode = true;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded = false;
    private bool rawGrounded = false;
    private float groundedTimer = 0f;
    private bool isDestroyed = false;
    private CameraManager cameraManager;
    private bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraManager = FindFirstObjectByType<CameraManager>();
    }

    void Update()
    {
        if (debuggingMode && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (isDestroyed || cameraManager.isLookingAround)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.angularVelocity = 0;
            return;
        }

        rawGrounded = Physics2D.OverlapBox(
            feetPos.position,
            new Vector2(landingWidth, landingHeight),
            transform.eulerAngles.z,
            groundLayer
        );

        if (rawGrounded)
        {
            groundedTimer = coyoteTime;
        }
        else
        {
            groundedTimer -= Time.deltaTime;
        }
        isGrounded = groundedTimer > 0f;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", horizontalInput);
    }

    void FixedUpdate()
    {
        if (isDestroyed || cameraManager.isLookingAround)
            return;

        if (isGrounded)
        {
            rb.gravityScale = 1;
            rb.freezeRotation = true;
            transform.eulerAngles = Vector3.zero;

            if (horizontalInput > 0)
            {
                canMove = true;
                rb.linearVelocityX = horizontalInput * landSpeed;
            }
            else
                rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0, landSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (!canMove)
                return;

            rb.freezeRotation = false;
            rb.gravityScale = 0.1f;

            rb.linearVelocity = rb.transform.right * flightSpeed;

            rb.AddTorque(-horizontalInput * torqueAmount, ForceMode2D.Impulse);

            float zRotation = transform.eulerAngles.z;
            if ((zRotation <= 60f && zRotation >= 0f) || (zRotation <= 360f && zRotation >= 300f))
                rb.AddTorque(-autoTorque, ForceMode2D.Force);
            else if (zRotation >= 120f && zRotation <= 240f)
                rb.AddTorque(autoTorque, ForceMode2D.Force);

            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxTorqueSpeed, maxTorqueSpeed);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, 10f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(feetPos.position, Quaternion.Euler(0, 0, transform.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(landingWidth, landingHeight, 0.01f));
        Gizmos.matrix = Matrix4x4.identity;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            isDestroyed = true;
            Debug.Log("Trigger");
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Ground")
        {
            isDestroyed = true;
            Debug.Log("Collision");
        }
    }
}
