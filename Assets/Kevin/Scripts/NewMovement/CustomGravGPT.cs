using Unity.VisualScripting;
using UnityEngine;

public class CustomGravGPT : MonoBehaviour
{
    public float gravity = -9.81f; // The strength of the gravity, negative value pulls objects down
    public float range = 10f; // The range at which objects will be affected by this gravity
    public bool pullPlayer = true; // Whether or not this gravity should affect the player

    private Collider[] colliders; // The colliders within range
    private Rigidbody playerRigidbody; // The player's rigidbody, if applicable

    private void Start()
    {
        // Get the colliders within range
        colliders = Physics.OverlapSphere(transform.position, range);
    }

    private void FixedUpdate()
    {
        foreach (Collider collider in colliders)
        {
            // If the collider has a rigidbody and is not this object, apply gravity
            if (collider.attachedRigidbody != null && collider.attachedRigidbody != GetComponent<Rigidbody>())
            {
                Vector3 gravityDirection = (transform.position - collider.transform.position).normalized;
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float gravityStrength = gravity / Mathf.Pow(distance, 2f);
                Vector3 gravityForce = gravityDirection * gravityStrength;

                collider.attachedRigidbody.AddForce(gravityForce, ForceMode.Acceleration);
            }

            // If this gravity should affect the player and the collider is the player, store the player's rigidbody
            if (pullPlayer && collider.CompareTag("Player"))
            {
                playerRigidbody = collider.attachedRigidbody;
            }
        }

        // If this gravity should affect the player and the player's rigidbody has been stored, apply gravity to the player
        if (pullPlayer && playerRigidbody != null)
        {
            Vector3 gravityDirection = (transform.position - playerRigidbody.transform.position).normalized;
            float distance = Vector3.Distance(transform.position, playerRigidbody.transform.position);
            float gravityStrength = gravity / Mathf.Pow(distance, 2f);
            Vector3 gravityForce = gravityDirection * gravityStrength;

            playerRigidbody.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to show the range of the gravity object
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
