using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay;
    public float explosionRadius;
    public float explosionForce;
    public float upModifier;
    public LayerMask interactionMask;
    public GameObject particlePrefab;
    private GameObject particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, interactionMask);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Destructable"))
            {
                Destroy(c.gameObject);
            } else if (c.CompareTag("Enemy"))
            {
                c.GetComponent<BotController>().GetDamage();
            }
        }

        particleSystem = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Invoke(nameof(Kill), 5);
    }

    void Kill()
    {
        Destroy(particleSystem);
        Destroy(gameObject);
    }
}
