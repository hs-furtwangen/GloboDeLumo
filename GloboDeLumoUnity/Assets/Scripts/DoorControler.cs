using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler: MonoBehaviour {

    //private Rigidbody rigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpening", true);
    }
}
