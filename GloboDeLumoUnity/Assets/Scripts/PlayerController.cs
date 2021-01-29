using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        forceDirection = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        {
            forceDirection.x = Input.GetAxis("Horizontal") * 100;
            forceDirection.y = 0;
            forceDirection.z = Input.GetAxis("Vertical") * 100;

            Vector3.ClampMagnitude(forceDirection, 0.7f);

            if (Physics.Raycast(transform.position, -Vector3.up, 1.05f))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    forceDirection.y = 200;
            }

            rb.AddForce(forceDirection);
        }
    }
}
