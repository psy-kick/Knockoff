using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float force = 10f; // the amount of force to apply
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // check if the collision is with a character or other solid object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("bounce");
            // apply a force in the opposite direction of the collision normal
            Vector3 normal = collision.contacts[0].normal;
            rb.AddForce(-normal * force, ForceMode.Impulse);
        }
    }
}
