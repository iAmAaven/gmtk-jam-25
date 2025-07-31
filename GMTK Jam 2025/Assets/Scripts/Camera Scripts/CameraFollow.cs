using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float pixelsPerUnit = 16f;
    public bool followTarget = true;

    public bool constrainX = false;
    public float minX;
    public float maxX;

    public bool constrainY = false;
    public float minY;
    public float maxY;

    public float followSpeed = 10f;
    private Transform target;

    void LateUpdate()
    {
        if (target == null)
        {
            if (!followTarget)
                target = transform;
            else
                target = GameObject.FindWithTag("Player").transform;

            return;
        }

        Vector3 targetPosition;

        if (followTarget)
            targetPosition = target.position + Vector3.up;
        else
            targetPosition = target.position;

        targetPosition.z = transform.position.z;

        Vector3 smoothPosition = Vector3.MoveTowards(transform.position, targetPosition, followSpeed);

        float unitsPerPixel = 1f / pixelsPerUnit;
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(smoothPosition.x / unitsPerPixel) * unitsPerPixel,
            Mathf.Round(smoothPosition.y / unitsPerPixel) * unitsPerPixel,
            -10
        );

        if (constrainX)
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minX, maxX);
        if (constrainY)
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minY, maxY);

        transform.position = smoothPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
