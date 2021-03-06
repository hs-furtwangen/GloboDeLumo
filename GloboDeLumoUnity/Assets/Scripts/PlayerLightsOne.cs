using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerLightsOne : MonoBehaviour, IBeatObject, IPlayerLightLevelController
{
    public GameObject BaseLight;
    public GameObject LightGroup;
    public GameObject SpotOne;
    private Light SpotOneLight;
    public GameObject TargetOne;
    public GameObject SpotTwo;
    private Light SpotTwoLight;
    public GameObject TargetTwo;

    public GameObject LevelOneStartTrigger;
    public GameObject LevelOneEndTrigger;
    
    private bool active;
    public Helper.ColorStates colorState;

    public float StartupDuration;
    public AnimationCurve StartupIntensity;
    public AnimationCurve StartupAngle;

    private float BaseIntensity;
    public float BeatIntensityVariance;
    private float BeatIntensity;

    public GameObject[] BlockersOnStartup;
    public GameObject[] BlockersOnShutdown;

    private LevelDj levelDj;

    public void Beat()
    {
        if (active)
        {
            if (BeatIntensity == BeatIntensityVariance)
            {
                BeatIntensity = 0;
            }
            else
            {
                BeatIntensity = BeatIntensityVariance;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        SpotOneLight = SpotOne.GetComponent<Light>();
        SpotTwoLight = SpotTwo.GetComponent<Light>();
        levelDj = GameObject.FindGameObjectWithTag("SoundController").GetComponent<LevelDj>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            SpotOneLight.intensity = BaseIntensity + BeatIntensity;
            SpotTwoLight.intensity = BaseIntensity + BeatIntensity;

            if (SpotOne != null && TargetOne != null)
                SpotOne.transform.LookAt(TargetOne.transform.position);

            if (SpotTwo != null && TargetTwo != null)
                SpotTwo.transform.LookAt(TargetTwo.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelOneLight")
        {
            if (other.gameObject == TargetOne)
            {
                SpotOne.SetActive(false);
                TargetOne.SetActive(false);

                colorState |= Helper.ColorStates.HasRed;

                if (colorState.HasFlag(Helper.ColorStates.HasBlue))
                {
                    BaseLight.GetComponent<Light>().color = new Color(1, 0, 1);
                }
                else
                {
                    BaseLight.GetComponent<Light>().color = new Color(1, 0, 0);
                }
            }
            if (other.gameObject == TargetTwo)
            {
                SpotTwo.SetActive(false);
                TargetTwo.SetActive(false);

                colorState |= Helper.ColorStates.HasBlue;

                if (colorState.HasFlag(Helper.ColorStates.HasRed))
                {
                    BaseLight.GetComponent<Light>().color = new Color(1, 0, 1);
                }
                else
                {
                    BaseLight.GetComponent<Light>().color = new Color(0, 0, 1);
                }
            }
        }
        else if (other.gameObject.tag == "LevelTrigger")
        {
            if (other.gameObject == LevelOneStartTrigger)
            {
                Startup();
            }
            if (other.gameObject == LevelOneEndTrigger)
            {
                Shutdown();
            }
        }

    }

    public void Startup()
    {
        if (!active)
        {
            LightGroup.SetActive(true);

            StartCoroutine(AnimateStartup(StartupDuration));

            active = true;

            levelDj.StartSongForLevel(1);

            if (BlockersOnStartup.Length != 0)
            {
                foreach (var block in BlockersOnStartup)
                {
                    if (block != null)
                        block.transform.position = Vector3.zero;
                }
            }
        }
    }

    public void Shutdown()
    {
        if (active)
        {
            LightGroup.SetActive(false);
            active = false;

            levelDj.FadeOutCurrentSong();

            if (BlockersOnShutdown.Length != 0)
            {
                foreach (var block in BlockersOnShutdown)
                {
                    if (block != null)
                        block.transform.position = Vector3.zero;
                }
            }
        }
    }

    IEnumerator AnimateStartup(float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float t = Mathf.Clamp01(journey / duration);

            SpotOneLight.intensity = StartupIntensity.Evaluate(t);
            SpotTwoLight.intensity = StartupIntensity.Evaluate(t);
            SpotOneLight.spotAngle = StartupAngle.Evaluate(t);
            SpotTwoLight.spotAngle = StartupAngle.Evaluate(t);

            BaseIntensity = SpotOneLight.intensity;

            yield return null;
        }
    }

    public Helper.ColorStates GetColorState()
    {
        return colorState;
    }

    public void ResetColorState()
    {
        colorState = 0;
    }
}
