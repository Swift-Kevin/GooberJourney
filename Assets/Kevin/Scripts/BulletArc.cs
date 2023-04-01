using UnityEngine;

public class BulletArc : MonoBehaviour
{
    public float speed = 10f;
    public float angle = 45f;
    public LayerMask arcLayerMask;

    private Vector3 target;
    private bool isArcing = false;

    public void Arc(Vector3 target)
    {
        this.target = target;
        isArcing = true;
    }

    void FixedUpdate()
    {
        if (isArcing)
        {
            // Calculate the distance and time it will take for the bullet to reach the target
            float distance = Vector3.Distance(transform.position, target);
            float time = distance / speed;

            // Calculate the initial velocity required to reach the target with the desired angle
            Vector3 direction = (target - transform.position).normalized;
            float gravity = Physics.gravity.magnitude;
            float initialVelocity = (distance / time) + (0.5f * gravity * time);
            Vector3 velocity = direction * initialVelocity;

            // Rotate the bullet to face the direction of travel
            transform.rotation = Quaternion.LookRotation(velocity);

            // Move the bullet along the calculated trajectory
            transform.position += velocity * Time.deltaTime;

            // Check if the bullet has hit an object with the arcLayerMask and destroy it
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f, arcLayerMask);
            if (hitColliders.Length > 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
