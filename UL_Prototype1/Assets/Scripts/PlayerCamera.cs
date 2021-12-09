using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 200f;
    private float xRotation = 0f;

    public GameObject player;
    private Vector3 _cameraOffset = new Vector3(0, 2, -5);
    private Vector3 _cameraOffsetCar = new Vector3(0, 5, -8);

    public bool isPlayerInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + _cameraOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!isPlayerInCar)
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void PlayerEnterCar()
    {
        isPlayerInCar = true;     
        transform.localRotation = Quaternion.Euler(10f, 0f, 0f);
        transform.position = player.transform.position + _cameraOffsetCar;
        StartCoroutine(AdjustCarCamera());
    }

    public void PlayerExitCar()
    {
        isPlayerInCar = false;
        transform.position = player.transform.position + _cameraOffset;
    }

    IEnumerator AdjustCarCamera()
    {
        yield return new WaitForSeconds(1f);

        transform.position = player.transform.position + _cameraOffsetCar;
    }
}
