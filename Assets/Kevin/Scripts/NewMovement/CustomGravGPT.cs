using UnityEngine;

public class CustomGravGPT : MonoBehaviour
{
    public float speed = 10f; // the speed of the bullet
    public float gravity = 9.8f; // the gravitational force applied to the bullet
    public LayerMask arcLayerMask; // the layer(s) that will trigger the arcing effect
    private Vector3 startPosition; // the position where the bullet was spawned
    private bool arcing = false; // flag indicating whether the bullet is currently arcing or not

    private void Start()
    {
        startPosition = transform.position; // save the initial position of the bullet
    }

    private void Update()
    {
        if (!arcing)
        {
            // if the bullet is not arcing, move it straight ahead
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // if the bullet is arcing, apply the gravitational force
            Vector3 direction = (transform.position - startPosition).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the bullet collides with an object on the arc layer mask, start arcing
        if (arcLayerMask == (arcLayerMask | (1 << other.gameObject.layer)))
        {
            arcing = true;
        }
    }
}
