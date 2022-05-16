using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;
    private float ySpeed;

    public float speed;
    public float turnSpeed;
    public float jumpSpeed;
    private Vector3 movementDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            ySpeed = -0.5f;   
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("jump");
                ySpeed = jumpSpeed;
            }
        }
        
        transform.Rotate(0, horizontalInput * turnSpeed * Time.deltaTime, 0);

        movementDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
