using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 forceDirection;

    private bool active;

    private PlayerLightController plc;

    // Start is called before the first frame update
    void Start()
    {
        active = false;

        rb = this.gameObject.GetComponent<Rigidbody>();
        forceDirection = Vector3.zero;
        plc = this.gameObject.GetComponent<PlayerLightController>();

        StartCoroutine(GiveControl());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active && rb != null)
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

    IEnumerator GiveControl()
    {
        yield return new WaitForSeconds(6f);

        plc.StartupLightForLevel(0);

        yield return new WaitForSeconds(3f);

        active = true;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LightReset")
        {
            plc.SetLightToWhite();
        }
    }
}
