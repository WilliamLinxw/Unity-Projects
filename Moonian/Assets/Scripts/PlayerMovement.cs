using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Rigidbody rb;
    public CharacterController controller;
    public Transform cam;

    public bool isRunning = false;
    public float movingVel = 3f;
    public float turnSmoothTime = .1f;

    public float slopeLimit = 45f;
    public bool allowJump = true;
    public float jumpSpeed = 6f;

    public bool isGrounded {get; private set;}
    // public float fwdInput {get; set;}
    // public float turnInput {get; set;}
    // public float jumpInput {get; set;}
    private CapsuleCollider capsuleCollider;

    private float turnSmoothVelocity;

    private int pressE = 1000;

    private float movingVel_;

    void Awake()
    {
        Instance = this;
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // todo: jump
        CheckGrounded();
        PerformActions();
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        // isRunning = Input.GetKey(KeyCode.LeftShift);
        // if (isRunning)
        // {
        //     movingVel_ = 2*movingVel;
        // }
        // else
        // {
        //     movingVel_ = movingVel;
        // }
        // Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // if (direction.magnitude >= 0.1f)
        // {
        //     float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //     transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
        //     Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //     controller.Move(moveDir.normalized * movingVel_ * Time.deltaTime);
        // }
    }

    private void CheckGrounded()
    {
        isGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    isGrounded = true;
            }
        }
    }

    private void PerformActions()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKey(KeyCode.Space);

        if (isRunning)
        {
            movingVel_ = 2*movingVel;
        }
        else
        {
            movingVel_ = movingVel;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (isGrounded)
        {
            Vector3 velocity = Vector3.zero;

            if (jump && allowJump)
            {
                Debug.Log("JJJump!");
                velocity += Vector3.up * jumpSpeed;
            }

            velocity += transform.forward * Mathf.Clamp(vertical, -1f, 1f) * movingVel_;
            controller.SimpleMove(velocity);
        }
        else
        {
            if (!Mathf.Approximately(vertical, 0f))
            {
                Vector3 verticalVel = Vector3.Project(rb.velocity, Vector3.up);
                Vector3 velocity = verticalVel + transform.forward * Mathf.Clamp(vertical, -1f, 1f) * movingVel_;
                controller.SimpleMove(velocity);
            }
        }
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressE = 0;
        }
        pressE += 1;
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Pickups") && other.gameObject.GetComponent<ItemController>().item.isCollectable && pressE <= 500)
        {
            bool interacted = other.gameObject.GetComponent<ItemController>().Interact();
            if (interacted)
            {
                other.gameObject.SetActive(false);
            }
            // Item item = other.gameObject.GetComponent<ItemController>().item;
            // InventoryManager.Instance.Add(item);

            pressE = 1000;
        }

        if (other.tag == "Door")
        {
            if (other.GetComponent<slidingDoor>().Moving == false)
            {
                other.GetComponent<slidingDoor>().Moving = true;
            }
        }

        if (other.tag == "Door_rec")
        {
            if (other.GetComponent<slidingDoor_rec>().Moving_rec == false)
            {
                other.GetComponent<slidingDoor_rec>().Moving_rec = true;
            }
        }
    }

}
