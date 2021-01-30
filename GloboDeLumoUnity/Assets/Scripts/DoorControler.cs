using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    public Helper.ColorStates[] openingConditions;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player")
        {
            var cntrl = other.transform.parent.GetComponent<PlayerLightsOne>();
            if (isSolved(cntrl))
            {
                openDoor();
            }
        }
    }

    private void openDoor()
    {
        const string variable = "isOpening";
        AudioSource audio = GetComponent<AudioSource>();

        // Play audio only once
        if (!animator.GetBool(variable))
        {
            audio.Play();
        }
        animator.SetBool(variable, true);
    }

    // Check if conditions are met
    private bool isSolved(PlayerLightsOne player)
    {
        foreach (Helper.ColorStates condition in openingConditions)
        {
            if (!player.colorState.HasFlag(condition))
            {
                return false;
            }

        }

        return true;
    }
}
