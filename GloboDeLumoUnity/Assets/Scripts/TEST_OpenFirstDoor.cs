using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_OpenFirstDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.tag == "Player")
        {
            var cntrl = other.transform.parent.GetComponent<PlayerLightsOne>();
            if(cntrl.colorState.HasFlag(PlayerLightsOne.ColorStates.HasBlue)
                && cntrl.colorState.HasFlag(PlayerLightsOne.ColorStates.HasRed))
            {
                transform.Translate(new Vector3(transform.localPosition.x, -40, transform.localPosition.z));
            }
        }
    }
}
