using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public bool disabled = false;

    public static Player Instance;
    private Animator anim;
    private CharacterController controller;
    private float ySpeed;
    public Camera cam;

    public bool isRunning = false;
    public float speed = 2f;
    public float speedDefault = 2f;
    public float turnSpeed;
    public float jumpSpeed;
    private Vector3 movementDirection = Vector3.zero;
    private bool jumpflag = false;

    private int pressE = 1000;
    public bool isJumping {get; private set;}
    private bool isGrounded;
    private bool isBackwards;
    private float camScale = -2f;
    private int fovMin = 45;
    private int fovMax = 120;
    private float overweightVelRatio = 0.6f;
    private float airtime = 0f;
    private float maxAirTimeWithoutDamage = 4f;
    private float fallingDamageConst = 6f;


    public bool atWorktable {get {return _atWorktable;}}
    private bool _atWorktable = false;

    public bool atRefueling {get {return _atRefueling;}}
    private bool _atRefueling;

    private Vector3 storedVel = Vector3.zero; // for storage and load

    void Awake()
    {
        // make sure only one Player instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
        speed = speedDefault;
        OverweightCheck();

        if (!disabled)
        {
            MotionUpdate(Time.deltaTime);
        }
        
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        PickUpCheck();
        FoVChangeCheck();
        LightCheck();

    }

    void OverweightCheck()
    {
        if (PlayerProperty.Instance.isOverweight)
        {
            speed = speed * overweightVelRatio;
        }
    }
    void PickUpCheck()
    {
        // pickup by press E
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressE = 0;
        }
        pressE += 1;
    }

    void FoVChangeCheck()
    {
        // mouse scroll as input
        if (Input.mouseScrollDelta.y != 0)
        {
            // fov change as scrolling
            float fov = cam.fieldOfView;
            float deltaFov = Input.mouseScrollDelta.y * camScale;
            fov += deltaFov;
            cam.fieldOfView = Mathf.Clamp(fov, fovMin, fovMax);
        }
    }
    void LightCheck()
    {
        // light check
        if (Input.GetButtonDown("Player Light Switch"))
        {
            PlayerProperty.Instance.playerLightOn = !PlayerProperty.Instance.playerLightOn;
            this.gameObject.transform.Find("PlayerLight").gameObject.SetActive(!this.gameObject.transform.Find("PlayerLight").gameObject.activeSelf);
        }
    }

    void MotionUpdate(float deltaTime)
    {
        // update for motions
        if (storedVel != Vector3.zero && !controller.isGrounded)
        {
            storedVel.y += Physics.gravity.y * deltaTime;
            controller.Move(storedVel * deltaTime);
        }
        if (controller.isGrounded)
        {
            storedVel = Vector3.zero;
            jumpflag = false;
            if (airtime > maxAirTimeWithoutDamage)
            {
                PlayerProperty.Instance.TakeDamage(airtime * fallingDamageConst);
            }
            airtime = 0;
        }
        else
        {
            airtime += deltaTime;
        }
        // Move forward
        if (Input.GetKey("w"))
        {
            // Accelerate
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetInteger("AnimationPar", 1);
            }
            else
            {
                anim.SetInteger("AnimationPar", 2);
            }
        }
        // Move backward
        else if (Input.GetKey("s"))
        {
            anim.SetInteger("AnimationPar", 3);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        // Assign the vertical speed
        ySpeed += Physics.gravity.y * deltaTime;

        // Only be able to jump when grounded
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

        // Change the speed when accelerate
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isBackwards = Input.GetKey("s");
        if (isRunning)
        {
            speed = speedDefault * 1.5f;
        }
        else if (isBackwards)
        {
            speed = speedDefault * 0.75f;
        }
        else
        {
            speed = speedDefault;
        }

        // Determine the direction
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

    // check if some triggers are active
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Pickups") && other.gameObject.GetComponent<ItemController>().item.isCollectable && pressE <= 200)
        {
            bool interacted = other.gameObject.GetComponent<ItemController>().Interact();  // adding to the inventory is integrated to the Interact()
            if (interacted)
            {
                other.gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("Pickup_1");
            }

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

        if (other.tag == "Worktable")
        {
            _atWorktable = true;
        }

        if (other.tag == "Finish")
        {
            _atRefueling = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        // reset the InBase and AtWorktable to false
        if (other.tag == "Base")
        {
            PlayerProperty.Instance.isInBase = false;
        }

        if (other.tag == "Worktable")
        {
            _atWorktable = false;
        }
        if (other.tag == "Finish")
        {
            _atRefueling = false;
        }
    }

    // position reset after loading or starting a new game
    public void SetPos(Vector3 pos)
    {
        controller.transform.position = pos;
        Physics.SyncTransforms();
    }

    public void SetStoredVel()
    {
        controller.SimpleMove(storedVel);
    }

    // store the velocity when a pause is called (e.g., during jumping)
    public void GetVel()
    {
        storedVel = controller.velocity;
        float len = Mathf.Sqrt(storedVel.x * storedVel.x + storedVel.z * storedVel.z);
        storedVel.x = storedVel.x / len * speed;
        storedVel.z = storedVel.z / len * speed;  // normalize to make sure the moving speed does not exceed the limit
    }
}
