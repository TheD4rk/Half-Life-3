using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesControl : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        animator.SetFloat("Roundness", Input.GetAxis("Horizontal"));
    }
}
