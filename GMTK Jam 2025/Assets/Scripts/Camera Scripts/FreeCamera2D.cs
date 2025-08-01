using UnityEngine;

public class FreeCamera2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 minLimits = new Vector2(-10f, -10f);
    public Vector2 maxLimits = new Vector2(10f, 10f);

    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, moveY, 0f).normalized;
        Vector3 newPos = transform.position + move * moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, minLimits.x, maxLimits.x);
        newPos.y = Mathf.Clamp(newPos.y, minLimits.y, maxLimits.y);
        transform.position = newPos;
    }
}
