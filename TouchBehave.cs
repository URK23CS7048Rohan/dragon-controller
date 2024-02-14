using UnityEngine;

public class TouchBehave : MonoBehaviour
{
    public Transform target; // Assign the dragon's transform to this field
    public float rotationSpeed = 3f;
    public float zoomSpeed = 5f;
    public float minZoomDistance = 5f;
    public float maxZoomDistance = 20f;

    private float currentZoom = 10f;
    private Vector2 lastTouchPosition;

    void Update()
    {
        HandleTouchInput();
        UpdateCameraPosition();
    }

    void HandleTouchInput()
    {
        // Check for touches
        if (Input.touchCount == 1)
        {
            // Single touch for rotation
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float horizontalRotation = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                target.Rotate(Vector3.up, horizontalRotation);
            }
        }
        else if (Input.touchCount == 2)
        {
            // Two touches for zoom
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Calculate the distance between the touches
            float touchDelta = Vector2.Distance(touch0.position, touch1.position);
            float deltaMagnitudeDiff = touchDelta - lastTouchPosition.magnitude;

            // Adjust the zoom based on the touch delta
            currentZoom -= deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoomDistance, maxZoomDistance);
        }

        // Store the last touch position for the next frame
        lastTouchPosition = Input.touches[0].position;
    }

    void UpdateCameraPosition()
    {
        // Calculate new camera position based on currentZoom
        Vector3 offset = new Vector3(0f, 0f, -currentZoom);
        Vector3 newPosition = target.position + offset;

        // Update the camera position
        transform.position = newPosition;

        // Look at the target (dragon)
        transform.LookAt(target);
    }
}
