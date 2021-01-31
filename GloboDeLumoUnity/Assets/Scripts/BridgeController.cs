using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Helper.ColorStates[] openingConditions;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.material.DisableKeyword("_EMISSION"));
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
                riseBridge();
            }
        }
    }

    private void riseBridge()
    {

        const string variable = "isRising";
        AudioSource audio = GetComponent<AudioSource>();

        // Play audio only once
        if (!animator.GetBool(variable))
        {
            audio.Play();
        }
        animator.SetBool(variable, true);

        transform.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.material.EnableKeyword("_EMISSION"));

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