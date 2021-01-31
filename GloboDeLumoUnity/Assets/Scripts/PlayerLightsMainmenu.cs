using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightsMainmenu : MonoBehaviour, IPlayerLightLevelController
{
    public GameObject BaseLight;
    private Transform baseLightTransform;
    private Light baseLightLight;

    public float StartupDuration;
    public AnimationCurve StartupIntensity;
    public AnimationCurve StartupRotX;
    public AnimationCurve StartupRotZ;

    private LevelDj levelDj;

    public void Start()
    {
        baseLightLight = BaseLight.GetComponent<Light>();
        baseLightLight.intensity = 0;
        baseLightTransform = BaseLight.transform;

        levelDj = GameObject.FindGameObjectWithTag("SoundController").GetComponent<LevelDj>();
    }

    public void Startup()
    {
        StartCoroutine(AnimateStartup(StartupDuration));
        baseLightLight.enabled = true;
    }

    public void Shutdown()
    {

    }

    IEnumerator AnimateStartup(float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float t = Mathf.Clamp01(journey / duration);

            baseLightLight.intensity = StartupIntensity.Evaluate(t);
            baseLightTransform.rotation = Quaternion.Euler(StartupRotX.Evaluate(t), StartupRotZ.Evaluate(t), 0);

            yield return null;
        }
    }

    public Helper.ColorStates GetColorState()
    {
        return Helper.ColorStates.None;
    }
}
