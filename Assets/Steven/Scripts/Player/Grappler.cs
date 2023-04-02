using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [Header("References")]
    private MovementScript pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask WhatisGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelaytime;
    public float overshootYAxis;
    private Vector3 grapplePoint;

    [Header("CoolDown")]
    public float grapplingCD;
    private float grapplingCDTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Q;
    private bool grappling;

    private void Start()
    {
        pm = GetComponent<MovementScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();
        if (grapplingCDTimer > 0)
        {
            grapplingCDTimer -= Time.deltaTime;
        }
      
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCDTimer > 0)
        {
            return;
        }
        grappling = true;
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, WhatisGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExercuteGrapple), grappleDelaytime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelaytime);
        }
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExercuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumptoPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        grappling = false;
        grapplingCDTimer = grapplingCD;
        lr.enabled = false;
    }
}
