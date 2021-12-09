using UnityEngine;

public class BlueVehicle : MonoBehaviour ,IVehicle
{
    private Rigidbody _rb;

    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _turnSpeed = 70f;
    [SerializeField] private float _breakThrust = 500f;
    private float _horizontalInput;

    [SerializeField] private int _movementState = 0;
    private bool _playerInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInCar)
        {
            //Make 2 buttons that can gear up and down.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(transform.forward * _breakThrust);
                _movementState = 0;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_movementState >= 0)
                    _movementState -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (_movementState <= 3)
                    _movementState += 1;
            }

            switch (_movementState)
            {
                case -1:
                    _moveSpeed = -10f;
                    _breakThrust = 0f;
                    break;
                case 0:
                    _moveSpeed = 0f;
                    _breakThrust = 0f;
                    break;
                case 1:
                    _moveSpeed = 10f;
                    _breakThrust = 2000f;
                    break;
                case 2:
                    _moveSpeed = 20f;
                    _breakThrust = 4000f;
                    break;
                case 3:
                    _moveSpeed = 30f;
                    _breakThrust = 6000f;
                    break;
                case 4:
                    _moveSpeed = 40f;
                    _breakThrust = 8000f;
                    break;
                default:
                    _moveSpeed = 0f;
                    break;
            }

            _horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if (_movementState != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _turnSpeed * _horizontalInput);
            _rb.MovePosition(transform.position + (transform.forward * _moveSpeed * Time.deltaTime));
        }
    }

    public void PlayerEnterCar()
    {
        _playerInCar = true;
    }

    public void PlayerExitCar()
    {
        _playerInCar = false;
        _rb.AddForce(transform.forward * _breakThrust);
        _movementState = 0;
    }
}
