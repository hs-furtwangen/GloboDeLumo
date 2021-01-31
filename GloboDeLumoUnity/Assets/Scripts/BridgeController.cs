using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Helper.ColorStates openingConditions;
    private Animator animator;
    private IPlayerLightLevelController[] iplc;

    public int LevelSelect0r;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.material.DisableKeyword("_EMISSION"));

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
    private bool isSolved()
    {
        if (openingConditions.HasFlag(Helper.ColorStates.HasRed) == iplc[LevelSelect0r].GetColorState().HasFlag(Helper.ColorStates.HasRed) &&
            openingConditions.HasFlag(Helper.ColorStates.HasGreen) == iplc[LevelSelect0r].GetColorState().HasFlag(Helper.ColorStates.HasGreen) &&
            openingConditions.HasFlag(Helper.ColorStates.HasBlue) == iplc[LevelSelect0r].GetColorState().HasFlag(Helper.ColorStates.HasBlue))
        {
            return true;
        }
        return false;
    }
   
}