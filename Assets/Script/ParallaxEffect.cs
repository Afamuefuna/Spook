using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    [Tooltip("Set it to 1 for the closest background, larger numbers for farther backgrounds")]
    public float parallaxFactor;

    private Vector3 previousTargetPosition;
    private float spriteWidth;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        previousTargetPosition = followTarget.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float deltaX = followTarget.position.x - previousTargetPosition.x;
        transform.position += Vector3.right * (deltaX / parallaxFactor);

        float distanceFromTarget = transform.position.x - followTarget.position.x;
        if (Mathf.Abs(distanceFromTarget) >= spriteWidth)
        {
            float offsetPositionX = (distanceFromTarget > 0) ? -spriteWidth : spriteWidth;
            transform.position = new Vector3(transform.position.x + offsetPositionX, transform.position.y, transform.position.z);
        }

        previousTargetPosition = followTarget.position;
    }
}