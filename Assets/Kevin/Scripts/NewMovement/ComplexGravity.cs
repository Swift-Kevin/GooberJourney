using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexGravity : MonoBehaviour
{
    public float gravity = -10f; // The strength of the gravity force
    public float maxDistance = 100f; // The maximum distance over which gravity can be applied
    public LayerMask mask; // The layers on which the gravity will act

    private Rigidbody rb; // The rigidbody component of the object

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the rigidbody component of the object
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance, mask); // Get all the colliders within the maximum distance and on the specified layers

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) // Skip the current object
            {
                continue;
            }

            Vector3 direction = transform.position - collider.transform.position; // Get the direction of the gravity force
            float distance = direction.magnitude; // Get the distance between the objects
            float strength = gravity / Mathf.Pow(distance, 2); // Calculate the strength of the gravity force

            rb.AddForce(direction.normalized * strength, ForceMode.Acceleration); // Apply the gravity force to the rigidbody component of the object
        }
    }
}