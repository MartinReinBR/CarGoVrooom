using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]private GameObject _explosion;

    public static event Action destroyedEvent;

    private void Destroyed()
    {
        if(destroyedEvent != null)
        {
            destroyedEvent?.Invoke();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Destroyed();
            Instantiate(_explosion, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
