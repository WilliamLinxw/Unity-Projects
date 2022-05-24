using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Animator anim;
    private CharacterController controller;
    private float ySpeed;
    public Camera cam;

    public bool isRunning = false;
    public float speed = 2f;
    public float turnSpeed;
    public float jumpSpeed;
    private Vector3 movementDirection = Vector3.zero;
    private bool jumpflag = false;

    private int pressE = 1000;
    private bool isJumping;
    private bool isGrounded;
    private bool isBackwards;
    private float camScale = -2f;
    private int fovMin = 45;
    private int fovMax = 120;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // MotionUpdate(Time.fixedDeltaTime);
    }

    void Update()
    {
        MotionUpdate(Time.deltaTime);
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressE = 0;
        }
        pressE += 1;

        if (Input.mouseScrollDelta.y != 0)
        {
            float fov = cam.fieldOfView;
            float deltaFov = Input.mouseScrollDelta.y * camScale;
            fov += deltaFov;
            cam.fieldOfView = Mathf.Clamp(fov, fovMin, fovMax);
        }
    }

    void MotionUpdate(float deltaTime)
    {
        if (controller.isGrounded)
        {
            jumpflag = false;
        }
        if (Input.GetKey("w"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetInteger("AnimationPar", 1);
            }
            else
            {
                anim.SetInteger("AnimationPar", 2);
            }
        }
        else if (Input.GetKey("s"))
        {
            anim.SetInteger("AnimationPar", 3);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        ySpeed += Physics.gravity.y * deltaTime;

        if (controller.isGrounded)
        {
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            anim.SetBool("IsJumping", false);
            isJumping = false;
            anim.SetBool("IsFalling", false);


            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("IsJumping", true);
                isJumping = true;
                jumpflag = true;
                Debug.Log("jump");
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            anim.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && ySpeed <0) || ySpeed < -2)
            {
                anim.SetBool("IsFalling", true);
            }
        }
        if (controller.isGrounded)
        {
            transform.Rotate(0, horizontalInput * turnSpeed * deltaTime, 0);
        }

        isRunning = Input.GetKey(KeyCode.LeftShift);
        isBackwards = Input.GetKey("s");
        if (isRunning)
        {
            speed = 3f;
        }
        else if (isBackwards)
        {
            speed = 1.5f;
        }
        else
        {
            speed = 2f;
        }

        movementDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        Vector3 velocity = Vector3.zero;
        if (controller.isGrounded && !jumpflag)
        {
            velocity = movementDirection * speed;
        }
        else
        {
            velocity = controller.velocity;
        }
        velocity.y = ySpeed;
        controller.Move(velocity * deltaTime);
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Pickups") && other.gameObject.GetComponent<ItemController>().item.isCollectable && pressE <= 500)
        {
            bool interacted = other.gameObject.GetComponent<ItemController>().Interact();
            if (interacted)
            {
                other.gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("Pickup_1");
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

        if (other.tag == "Base")
        {
            PlayerProperty.Instance.isInBase = true;
        }
    }
}
