using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(Light))]
public class LightGunController : MonoBehaviour
{

    [SerializeField, Tooltip("The angle of the light"), Range(-90.0f, 90.0f), DefaultValue(0.0f)]
    private float lightRotationAngle;

    [SerializeField, Tooltip("The speed the light rotates"), Range(10.0f, 30.0f)]
    private float rotationSpeed = 15f;


    [SerializeField, Tooltip("Spotlight GameObject")]
    private GameObject spotlightObject;

    [SerializeField, Tooltip("Spotlight")]
    private Light spotlight;

    void Start()
    {
        // Make sure spotlightObject is assigned or use the current GameObject
        spotlightObject = spotlightObject != null ? spotlightObject : gameObject;

        // Try to get the Light component from the spotlightObject
        spotlight = spotlightObject.GetComponent<Light>();

        if (spotlight == null)
        {
            Debug.LogError("Spotlight Light component not found!");
        }
        else
        {
            // Set the light type to Spotlight
            spotlight.type = UnityEngine.LightType.Spot;

            // Set the initial rotation to face vertically down
            spotlightObject.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RotateTowardsCursor();
        }
    }
    void RotateTowardsCursor()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Cast a ray from the camera to the mouse position in world coordinates
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object in the scene
        if (Physics.Raycast(ray, out hit))
        {
            // Get the target position where the gun should point
            Vector3 targetPosition = hit.point;

            // Use LookAt to align the gun's forward direction with the target position
            transform.LookAt(targetPosition);

            // Rotate the spotlight based on the gun's rotation
            RotateSpotlight(transform.rotation);
        }
    }


    void RotateSpotlight(Quaternion targetRotation)
    {
        // Make sure the spotlightObject is assigned
        if (spotlightObject != null)
        {
            // Rotate the spotlight to match the gun's rotation
            spotlightObject.transform.rotation = targetRotation;
        }
    }
}
