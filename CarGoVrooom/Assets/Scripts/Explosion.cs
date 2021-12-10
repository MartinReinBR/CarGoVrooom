using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem _explosion;

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
