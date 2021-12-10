using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]private float _mouseSensitivity = 150f;
    private float _xRotation = 0f;

    [SerializeField] private GameObject _player;
    private Vector3 _cameraOffset = new Vector3(0, 2, -5);
    private Vector3 _cameraOffsetCar = new Vector3(0, 5, -8);

    public bool isPlayerInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _player.transform.position + _cameraOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!isPlayerInCar)
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _player.transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void PlayerEnterCar()
    {
        isPlayerInCar = true;     
        StartCoroutine(AdjustCarCamera(true));
    }

    public void PlayerExitCar()
    {
        isPlayerInCar = false;
        StartCoroutine(AdjustCarCamera(false));
    }

    IEnumerator AdjustCarCamera(bool enteringCar)
    {
        yield return new WaitForSeconds(0.05f);

        if (enteringCar)
        {
            transform.localRotation = Quaternion.Euler(10f, 0f, 0f);
            transform.localPosition = _cameraOffsetCar;
        }
        else if (!enteringCar)
        {
            transform.localPosition = _cameraOffset;
        }

    }
}
