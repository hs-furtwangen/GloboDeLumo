using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    public Helper.ColorStates[] openingConditions;
    private Animator animator;

    private IPlayerLightLevelController[] iplc;
    public int LevelSelect0r;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        iplc = new IPlayerLightLevelController[4];
        iplc[0] = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightsMainmenu>();
        iplc[1] = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightsOne>();
        iplc[2] = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightsTwo>();
        iplc[3] = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightsThree>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player")
        {
            if (isSolved())
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
    private bool isSolved()
    {
        foreach (Helper.ColorStates condition in openingConditions)
        {
            if (!iplc[LevelSelect0r].GetColorState().HasFlag(condition))
            {
                return false;
            }

        }

        return true;
    }
}
