using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody rb;
    public Collider _collider;
    [SerializeField]public GameObject playerCamera;
    public GameObject CurrentCar;

    [SerializeField] private float moveSpeed = 20f;
    private float horizontalInput;
    private float forwardInput;

    private bool canEnterCar = false;
    public bool isInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical inputs 
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        forwardInput = Input.GetAxis("Vertical") * moveSpeed;

        if (canEnterCar && Input.GetKeyDown(KeyCode.E) && !isInCar)
        {
            isInCar = true;
            _collider.isTrigger = true;
            CurrentCar.GetComponent<IVehicle>().PlayerEnterCar();
            playerCamera.GetComponent<PlayerCamera>().PlayerEnterCar();
        }

        else if(isInCar && Input.GetKeyDown(KeyCode.E))
        {
            isInCar = false;
            canEnterCar = false;
            _collider.isTrigger = false;
            CurrentCar.GetComponent<IVehicle>().PlayerExitCar();
            playerCamera.GetComponent<PlayerCamera>().PlayerExitCar();
            transform.position = CurrentCar.transform.position + new Vector3(-4f,5f,0f);
            CurrentCar = null;
        }

        if (isInCar)
        {
            transform.position = CurrentCar.transform.position;
            transform.rotation = CurrentCar.transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (!isInCar)
        {
            Vector3 move = transform.right * horizontalInput + transform.forward * forwardInput;
            rb.MovePosition(transform.position + (move * Time.deltaTime));
            //rb.velocity = move;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collision Enter");
            canEnterCar = true;
            CurrentCar = collision.gameObject;  
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && !isInCar)
        {
            Debug.Log("Collision exit");
            canEnterCar = false;
            CurrentCar = null;
        }
    }
}
