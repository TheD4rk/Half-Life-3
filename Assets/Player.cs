using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject ballPrefab;
    public Transform hand;
    public float throwForce;
    public Camera cam;
    public LayerMask interactionMask;
    public float maxDistance;
    private GameObject objInHand;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(cubePrefab, hand.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject ball = Instantiate(ballPrefab, hand.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (objInHand == null)
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(maxDistance));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance, interactionMask))
                {
                    objInHand = hit.transform.gameObject;
                    objInHand.transform.position = hand.position;
                    objInHand.GetComponent<Rigidbody>().isKinematic = true;
                    objInHand.transform.parent = hand;
                }
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (objInHand != null)
            {
                objInHand.GetComponent<Rigidbody>().isKinematic = false;
                objInHand.transform.parent = null;
                objInHand.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);

                objInHand = null;
            }
        }
    }
}
