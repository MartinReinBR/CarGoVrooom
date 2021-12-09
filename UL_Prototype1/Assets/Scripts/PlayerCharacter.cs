using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject UI;
    [SerializeField]private GameObject _playerCamera;
    [SerializeField]private GameObject _currentCar;

    [SerializeField] private float _moveSpeed = 20f;
    private float _horizontalInput;
    private float _forwardInput;

    private bool _canEnterCar = false;
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
        _horizontalInput = Input.GetAxis("Horizontal") * _moveSpeed;
        _forwardInput = Input.GetAxis("Vertical") * _moveSpeed;

        if (_canEnterCar && Input.GetKeyDown(KeyCode.E) && !isInCar)
        {
            isInCar = true;
            _collider.isTrigger = true;
            _currentCar.GetComponent<IVehicle>().PlayerEnterCar();
            _playerCamera.GetComponent<PlayerCamera>().PlayerEnterCar();
            UI.GetComponent<UI>().SetEnterText(false);
        }

        else if(isInCar && Input.GetKeyDown(KeyCode.E))
        {
            isInCar = false;
            _canEnterCar = false;
            UI.GetComponent<UI>().SetEnterText(false);
            _collider.isTrigger = false;
            _currentCar.GetComponent<IVehicle>().PlayerExitCar();
            _playerCamera.GetComponent<PlayerCamera>().PlayerExitCar();
            transform.position = _currentCar.transform.position + new Vector3(-4f,5f,0f);
            _currentCar = null;
        }

        if (isInCar)
        {
            transform.position = _currentCar.transform.position;
            transform.rotation = _currentCar.transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (!isInCar)
        {
            Vector3 move = transform.right * _horizontalInput + transform.forward * _forwardInput;
            rb.MovePosition(transform.position + (move * Time.deltaTime));
            //rb.velocity = move;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            UI.GetComponent<UI>().SetEnterText(true);
            Debug.Log("Collision Enter");
            _canEnterCar = true;
            _currentCar = collision.gameObject;  
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && !isInCar)
        {
            UI.GetComponent<UI>().SetEnterText(false);
            _canEnterCar = false;
            _currentCar = null;
        }
    }
}
