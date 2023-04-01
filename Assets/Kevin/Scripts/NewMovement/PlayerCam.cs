using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    PlayerMovement inputCtrls;

    public Transform mesh;
    public Transform orientation;

    public float sensX;
    public float sensY;


    float xRotation;
    float yRotation;

    private void Start()
    {
        inputCtrls = new PlayerMovement();
        inputCtrls.Player.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float mouseX = inputCtrls.Player.Cam.ReadValue<Vector2>().x * sensX;
        float mouseY = inputCtrls.Player.Cam.ReadValue<Vector2>().y * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mesh.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        Physics.SyncTransforms();
    }
}
