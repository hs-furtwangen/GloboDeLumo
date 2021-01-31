using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody rb;

    public float ForceStrength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) % 20 == 0)
            rb.AddForce(new Vector3(0, ForceStrength, 0), ForceMode.Impulse);
    }
}
