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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(cubePrefab, hand.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject ball = Instantiate(ballPrefab, hand.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
