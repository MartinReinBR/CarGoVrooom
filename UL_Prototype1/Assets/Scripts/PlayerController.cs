using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 20f;
    [SerializeField]private float turnSpeed = 30f;
    private float horizontalInput;
    private float forwardInput;

    public int movementState = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            movementState = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            movementState = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            movementState = 2;
        }

        switch (movementState)
        {
            case 0:
                moveSpeed = 0f;
                break;
            case 1:
                moveSpeed = 15f;
                break;
            case 2:
                moveSpeed = 30f;
                break;
            default:
                moveSpeed = 0f;
                break;
        }


        horizontalInput = Input.GetAxisRaw("Horizontal");
        //forwardInput = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

}
