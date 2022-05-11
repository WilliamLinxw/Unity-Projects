using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public CharacterController controller;
    public Transform cam;

    public bool isRunning = false;
    public float movingVel = 3f;
    public float turnSmoothTime = .1f;

    private float turnSmoothVelocity;

    private int pressE = 1000;

    private float movingVel_;

    void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
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
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movingVel_ * Time.deltaTime);
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
