using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSwAnimations : MonoBehaviour
{
    PlayerMovement playerMovement;
    public bool isAiming;
    public Animator aimShooting;
    [SerializeField] GameObject thirdPersonCamera;
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
            thirdPersonCamera.SetActive(false);
            aimDownSightsCamera.SetActive(true);
            aimShooting.SetBool("Aim", true);
        }
        else if (playerMovement.Player.ADSBool.WasReleasedThisFrame())
        {
            isAiming = false;
            thirdPersonCamera.SetActive(true);
            aimDownSightsCamera.SetActive(false);
            aimShooting.SetBool("Aim", false);
        }
    }
}
