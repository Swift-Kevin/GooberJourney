using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = 9.8f; // adjust to change gravity strength

    private Rigidbody playerRb;
    private bool isGrounded;
    private GameObject currentGround;

    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerRb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 gravityForce = currentGround.transform.up * (-gravity * playerRb.mass);
            playerRb.AddForce(gravityForce);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            currentGround = other.gameObject;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && other.gameObject == currentGround)
        {
            isGrounded = false;
            currentGround = null;
        }
    }
}