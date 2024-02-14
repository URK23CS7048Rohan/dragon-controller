using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private float speed;
    private FixedJoystick fixedJoystick;
    private Rigidbody rigidbody;

    private void Start()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (fixedJoystick == null)
        {
            Debug.LogError("Fixed Joystick not found in the scene. Make sure it's correctly set up.");
            return;
        }

        HandleInput();
    }

    private void HandleInput()
    {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        // Check if joystick input is within a reasonable range
        if (Mathf.Abs(xVal) > 0.1f || Mathf.Abs(yVal) > 0.1f)
        {
            Vector3 movement = new Vector3(xVal, 0, yVal).normalized;

            // Calculate the rotation angle based on the joystick input
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

            // Rotate the dragon to face the target angle
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Move the dragon in the forward direction
            rigidbody.velocity = transform.forward * speed;
        }
        else
        {
            // If there's no input, dampen the velocity to make the dragon gradually come to a stop
            rigidbody.velocity *= 0.95f; // You can experiment with the damping factor

            // If the velocity is very small, set it to zero to prevent slow drifting
            if (rigidbody.velocity.magnitude < 0.01f)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
    }
}