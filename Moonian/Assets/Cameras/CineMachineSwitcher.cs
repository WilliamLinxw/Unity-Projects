using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMachineSwitcher : MonoBehaviour
{
    private Animator animator;
    private bool thirdPersonCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void SwicthState()
    {
        if (thirdPersonCamera)
        {
            animator.Play("Indoor Camera");
        }
        else
        {
            animator.Play("Third Person Camera");
        }
        thirdPersonCamera = !thirdPersonCamera;
        Debug.Log("Switched to " + thirdPersonCamera);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SwicthState();
        }
    }
}
