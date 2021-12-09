using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem _explosion;

    // Start is called before the first frame update
    private void Awake()
    {
        _explosion = GetComponent<ParticleSystem>();
        StartCoroutine(PlayExplosion());
    }

    private IEnumerator PlayExplosion()
    {
        _explosion.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
