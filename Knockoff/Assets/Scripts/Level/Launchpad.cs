using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    [SerializeField] private float force = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if the collision is with a character or other solid object
        if (collision.gameObject.CompareTag("Player"))
        {
            // apply a force in the opposite direction of the collision normal
            Vector3 normal = collision.contacts[0].normal;
            rb.AddForce(-normal * force, ForceMode.Impulse);
        }
    }
}
