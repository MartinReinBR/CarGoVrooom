using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueVehicle : MonoBehaviour ,IVehicle
{
    public Rigidbody rb;

    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float turnSpeed = 30f;
    [SerializeField] private float breakThrust = 500f;
    private float horizontalInput;
    private float forwardInput;

    public int movementState = 0;
    public bool playerInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInCar)
        {
            //Make 2 buttons that can gear up and down.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.forward * breakThrust);
                movementState = 0;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (movementState >= 0)
                    movementState -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (movementState <= 3)
                    movementState += 1;
            }

            switch (movementState)
            {
                case -1:
                    moveSpeed = -10f;
                    breakThrust = 0f;
                    break;
                case 0:
                    moveSpeed = 0f;
                    breakThrust = 0f;
                    break;
                case 1:
                    moveSpeed = 10f;
                    breakThrust = 2000f;
                    break;
                case 2:
                    moveSpeed = 20f;
                    breakThrust = 4000f;
                    break;
                case 3:
                    moveSpeed = 30f;
                    breakThrust = 6000f;
                    break;
                case 4:
                    moveSpeed = 40f;
                    breakThrust = 8000f;
                    break;
                default:
                    moveSpeed = 0f;
                    break;
            }

            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if (movementState != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
            rb.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
        }
    }

    public void PlayerEnterCar()
    {
        playerInCar = true;
    }

    public void PlayerExitCar()
    {
        playerInCar = false;
        rb.AddForce(transform.forward * breakThrust);
        movementState = 0;
    }
}
