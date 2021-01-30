using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public ColorStates colorState;

    public float StartupDuration;
    public AnimationCurve StartupIntensity;
    public AnimationCurve StartupAngle;

    public void Beat()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        SpotOneLight = SpotOne.GetComponent<Light>();
        SpotTwoLight = SpotTwo.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
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

                colorState |= ColorStates.HasRed;

                if (colorState.HasFlag(ColorStates.HasBlue))
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

                colorState |= ColorStates.HasBlue;

                if (colorState.HasFlag(ColorStates.HasRed))
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
        }
    }

    public void Shutdown()
    {
        if (active)
        {
            LightGroup.SetActive(false);
            active = false;
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

            yield return null;
        }
    }

    [Flags]
    public enum ColorStates
    {
        None,
        HasRed,
        HasBlue
    }
}
