using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    PlayerMovement playerMovement;
    public bool isAiming;
    [SerializeField] GameObject firstPersonCamera;
    [SerializeField] GameObject aimDownSightsCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = new PlayerMovement();
        playerMovement.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        AimDownSights();
    }

    void AimDownSights()
    {
        if (playerMovement.Player.ADSBool.WasPressedThisFrame())
        {
            // boolean for Steven to use for animations, if he can use it
            isAiming = true;
            firstPersonCamera.SetActive(false);
            aimDownSightsCamera.SetActive(true);
        }
        else if (playerMovement.Player.ADSBool.WasReleasedThisFrame())
        {
            isAiming = false;
            firstPersonCamera.SetActive(true);
            aimDownSightsCamera.SetActive(false);
        }
    }
}
