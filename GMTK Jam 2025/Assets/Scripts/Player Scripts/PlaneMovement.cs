using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneMovement : MonoBehaviour
{
    [Header("Flight")]
    public float flightSpeed = 10f;
    public float torqueAmount = 0.025f, maxTorqueSpeed = 100f, autoTorque = 0.05f;
    public float unchangedFlightSpeed = 10f;
    public float unchangedMaxTorque = 100f;

    [Header("Gas")]
    public int maxGas = 20;
    public int currentGas;
    public bool isOutOfGas = false;
    public float gasConsumption = 1f, unchangedGasConsumption = 1f;

    [Header("Landed Movement")]
    public float landSpeed;
    public float landingWidth, landingHeight;
    public float coyoteTime = 0.1f;

    [Header("References")]
    public Transform feetPos;
    public Animator animator;
    public LayerMask groundLayer;
    public TrailRenderer bigTrail, smolTrail;
    public bool debuggingMode = true;
    public bool hasStartedMoving = false;
    public bool movementIsPrevented = false;


    // PRIVATES
    private Rigidbody2D rb;
    private CameraManager cameraManager;
    private PetrolMeter petrolMeter;
    private float horizontalInput;
    private float gasTimer = 0f;
    public bool isGrounded = false;
    private bool rawGrounded = false;
    private float groundedTimer = 0f;
    [HideInInspector] public bool isDestroyed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        petrolMeter = FindFirstObjectByType<PetrolMeter>();
        currentGas = maxGas;

        petrolMeter.RefreshPetrolMeter(currentGas, maxGas);
    }

    void Update()
    {
        if (debuggingMode && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", rb.linearVelocityX);

        HandleGrounding();

        if (!isGrounded && !isDestroyed && Time.time >= gasTimer)
        {
            gasTimer = Time.time + gasConsumption;
            ConsumeFuel();
        }

        if (cameraManager.isLookingAround)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.angularVelocity = 0;
            return;
        }
    }

    void FixedUpdate()
    {
        if (isDestroyed || cameraManager.isLookingAround)
            return;

        if (isGrounded)
            GroundedMovement();
        else
            FlightMovement();
    }

    void FlightMovement()
    {
        rb.freezeRotation = false;
        rb.gravityScale = 0.1f;
        bigTrail.time = 1f;
        smolTrail.time = 0.5f;

        if (!hasStartedMoving || isOutOfGas || movementIsPrevented)
            return;

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

    void GroundedMovement()
    {
        rb.gravityScale = 10f;
        rb.freezeRotation = true;
        transform.eulerAngles = Vector3.zero;
        bigTrail.time = Mathf.MoveTowards(bigTrail.time, 0, Time.fixedDeltaTime);
        smolTrail.time = Mathf.MoveTowards(smolTrail.time, 0, Time.fixedDeltaTime);

        if (movementIsPrevented)
            return;

        if (horizontalInput > 0)
        {
            hasStartedMoving = true;
            rb.linearVelocityX = horizontalInput * landSpeed;
        }
        else
            rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0, landSpeed * Time.fixedDeltaTime);
    }

    void HandleGrounding()
    {
        rawGrounded = Physics2D.OverlapBox(
            feetPos.position,
            new Vector2(landingWidth, landingHeight),
            transform.eulerAngles.z,
            groundLayer
        );

        if (rawGrounded)
        {
            groundedTimer = coyoteTime;
            gasTimer = Time.time + gasConsumption;
        }
        else
        {
            groundedTimer -= Time.deltaTime;
        }
        isGrounded = groundedTimer > 0f;

        animator.SetBool("IsGrounded", isGrounded);
    }

    public void RefuelTank()
    {
        isOutOfGas = false;
        currentGas = maxGas;
        petrolMeter.RefreshPetrolMeter(currentGas, maxGas);
    }

    public void ConsumeFuel()
    {
        currentGas--;
        if (currentGas <= 0)
        {
            currentGas = 0;
            isOutOfGas = true;
        }

        petrolMeter.RefreshPetrolMeter(currentGas, maxGas);
    }

    public void DestroyPlane()
    {
        isDestroyed = true;
        StopPlane();
        GameManager.Instance.motivationManager.LoseMotivation();
    }

    public void StopPlane()
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(feetPos.position, Quaternion.Euler(0, 0, transform.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(landingWidth, landingHeight, 0.01f));
        Gizmos.matrix = Matrix4x4.identity;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string colTag = coll.gameObject.tag;
        if (colTag != "Player" && colTag != "Ground" && colTag != "PlaneTrigger"
            && colTag != "PickUp" && colTag != "Tutorial")
        {
            if (!isDestroyed)
                DestroyPlane();
            Debug.Log("Collision");
        }
    }
}
