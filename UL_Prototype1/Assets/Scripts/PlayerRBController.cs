using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRBController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float turnSpeed = 30f;
    private float horizontalInput;
    private float forwardInput;

    public int movementState = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make 2 buttons that can gear up and down.
        if (Input.GetKeyDown(KeyCode.L))
        {
            movementState = 0;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(movementState >=0)
                movementState -= 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(movementState <= 3)
                movementState += 1;
        }

        switch (movementState)
        {
            case -1:
                moveSpeed = -10f;
                break;
            case 0:
                moveSpeed = 0f;
                break;
            case 1:
                moveSpeed = 10f;
                break;
            case 2:
                moveSpeed = 20f;
                break;
            case 3:
                moveSpeed = 30f;
                break;
            case 4:
                moveSpeed = 40f;
                break;
            default:
                moveSpeed = 0f;
                break;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        //forwardInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(movementState != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
            rb.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
        }
    }
}
