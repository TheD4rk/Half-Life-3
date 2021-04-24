using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool buildMode;
    private Camera cam;
    private float maxDistance;
    
    public LayerMask interactionMask;

    private void Start()
    {
        buildMode = false;
        maxDistance = 100f;
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchBuildMode();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            buildMode = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            buildMode = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            buildMode = true;
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawLine(ray.origin, ray.GetPoint(maxDistance));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, interactionMask))
        {
            if (hit.collider.CompareTag("Destructable"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void SwitchBuildMode()
    {
        buildMode = !buildMode;
    }
    
    
}
