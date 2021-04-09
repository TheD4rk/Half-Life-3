using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanControlls : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Crouch");
        }
        
        animator.SetFloat("Forward", Input.GetAxis("Horizontal"));
        animator.SetFloat("Sideward", Input.GetAxis("Vertical"));
    }
}
