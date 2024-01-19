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
    [SerializeField, Tooltip("The speed the light rotates"), Range(10.0f, 30.0f)]
    private float rotationSpeed = 15f;

    [SerializeField, Tooltip("Spotlight GameObject")]
    private GameObject spotlightObject;

    [SerializeField, Tooltip("The range of the light"), Range(10.0f, 30.0f)]
    private float maxLightRange;

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
        RotateTowardsCursor();

        // Check for activation input (e.g., left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            ActivateLight();
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

    void ActivateLight()
    {
        Debug.Log("ActivateLight method called.");

        Vector3 lightDirection = spotlightObject.transform.forward;

        // Debugging: Draw a green ray to visualize the direction
        Debug.DrawRay(transform.position, lightDirection * maxLightRange, Color.green, 2f);

        // Perform a raycast to check for hits on enemies
        RaycastHit[] hits = Physics.RaycastAll(transform.position, lightDirection, maxLightRange);

        if (hits.Length > 0)
        {
            Debug.Log("Ray hit something!");

            foreach (var hit in hits)
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                Debug.Log("Hit Point: " + hit.point);
                Debug.Log("Hit Normal: " + hit.normal);
            }
        }
        else
        {
            Debug.Log("Ray hit nothing.");
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
