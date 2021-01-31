using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightsThree : MonoBehaviour, IPlayerLightLevelController, IBeatObject
{
    public GameObject BaseLight;
    public GameObject LightGroup;

    public GameObject TargetOne;
    public GameObject TargetTwo;
    public GameObject TargetThree;

    public GameObject LevelThreeStartTrigger;
    public GameObject LevelThreeEndTrigger;

    private GameObject[] Lights;
    public bool active;
    public bool rotate;

    public Helper.ColorStates colorState;

    private float BaseIntensity = 2;
    public float BeatIntensityVariance;
    private float BeatIntensity1;
    private float BeatIntensity2;

    public GameObject[] BlockersOnStartup;
    public GameObject[] BlockersOnShutdown;

    private LevelDj levelDj;

    public void Shutdown()
    {
        if (active)
        {
            rotate = true;
            BaseLight.GetComponent<Light>().intensity = 0;

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

    public void Startup()
    {
        if (!active)
        {
            LightGroup.SetActive(true);
            BaseLight.GetComponent<Light>().color = Color.white;
            active = true;

            levelDj.StartSongForLevel(3);

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

    public Helper.ColorStates GetColorState()
    {
        return colorState;
    }

    public void ResetColorState()
    {
        colorState = Helper.ColorStates.None;
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        rotate = false;
        Lights = new GameObject[32];

        var angle = 360f / Lights.Length;
        var radius = 0.1f;

        levelDj = GameObject.FindGameObjectWithTag("SoundController").GetComponent<LevelDj>();

        for (int i = 0; i < Lights.Length; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 position = direction * radius;

            var go = new GameObject();

            go.transform.parent = LightGroup.transform;
            go.transform.localPosition = position;
            go.transform.localRotation = Quaternion.Euler(new Vector3(0, i * angle, 0));

            var comp = go.AddComponent<Light>();
            comp.type = LightType.Spot;
            comp.intensity = 10;
            comp.spotAngle = 10;
            comp.range = 20;
            comp.shadows = LightShadows.Hard;
            comp.shadowBias = 0.01f;
            comp.shadowNearPlane = 0.1f;

            Lights[i] = go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            var redDir = (TargetOne.transform.position - this.transform.position).normalized;
            var greenDir = (TargetTwo.transform.position - this.transform.position).normalized;
            var blueDir = (TargetThree.transform.position - this.transform.position).normalized;

            Debug.DrawLine(this.transform.position, this.transform.position + redDir * 10, Color.red);
            Debug.DrawLine(this.transform.position, this.transform.position + greenDir * 10, Color.green);
            Debug.DrawLine(this.transform.position, this.transform.position + blueDir * 10, Color.blue);

            for (int i = 0; i < Lights.Length; i++)
            {
                var light = Lights[i].GetComponent<Light>();

                var redDistance = Vector3.Distance(redDir, Lights[i].transform.localPosition.normalized);
                var greenDistance = Vector3.Distance(greenDir, Lights[i].transform.localPosition.normalized);
                var blueDistance = Vector3.Distance(blueDir, Lights[i].transform.localPosition.normalized);

                var colorVector = new Vector3(1 / redDistance, 1 / greenDistance, 1 / blueDistance).normalized;

                var color = new Color(colorVector.x, colorVector.y, colorVector.z, 1);

                light.color = color;

                if (i % 2 == 0)
                {
                    light.intensity = BaseIntensity + BeatIntensity1;
                }
                else
                {
                    light.intensity = BaseIntensity + BeatIntensity2;
                }
            }

            if (rotate)
            {
                LightGroup.transform.Rotate(Vector3.up * Time.deltaTime * 35);
            }
        }
    }

    public void Beat()
    {
        if (active)
        {
            if (BeatIntensity1 == BeatIntensityVariance)
            {
                BeatIntensity1 = 0;
                BeatIntensity2 = BeatIntensityVariance;
            }
            else
            {
                BeatIntensity1 = BeatIntensityVariance;
                BeatIntensity2 = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelOneLight")
        {
            if (other.gameObject == TargetOne)
            {
                colorState |= Helper.ColorStates.HasRed;

                if (colorState.HasFlag(Helper.ColorStates.HasGreen | Helper.ColorStates.HasBlue))
                {
                    BaseLight.GetComponent<Light>().color = Color.white;
                    colorState = 0;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasGreen))
                {
                    BaseLight.GetComponent<Light>().color = Color.yellow;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasBlue))
                {
                    BaseLight.GetComponent<Light>().color = Color.magenta;
                }
                else
                {
                    BaseLight.GetComponent<Light>().color = Color.red;
                }
            }
            else if (other.gameObject == TargetTwo)
            {
                colorState |= Helper.ColorStates.HasGreen;

                if (colorState.HasFlag(Helper.ColorStates.HasRed | Helper.ColorStates.HasBlue))
                {
                    BaseLight.GetComponent<Light>().color = Color.white;
                    colorState = 0;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasRed))
                {
                    BaseLight.GetComponent<Light>().color = Color.yellow;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasBlue))
                {
                    BaseLight.GetComponent<Light>().color = Color.cyan;
                }
                else
                {
                    BaseLight.GetComponent<Light>().color = Color.green;
                }

            }
            else if (other.gameObject == TargetThree)
            {
                colorState |= Helper.ColorStates.HasBlue;

                if (colorState.HasFlag(Helper.ColorStates.HasGreen | Helper.ColorStates.HasRed))
                {
                    BaseLight.GetComponent<Light>().color = Color.white;
                    colorState = 0;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasGreen))
                {
                    BaseLight.GetComponent<Light>().color = Color.cyan;
                }
                else if (colorState.HasFlag(Helper.ColorStates.HasRed))
                {
                    BaseLight.GetComponent<Light>().color = Color.magenta;
                }
                else
                {
                    BaseLight.GetComponent<Light>().color = Color.blue;
                }
            }
        }
        else if (other.gameObject.tag == "LevelTrigger")
        {
            if (other.gameObject == LevelThreeStartTrigger)
            {
                Startup();
            }
            if (other.gameObject == LevelThreeEndTrigger)
            {
                Shutdown();
            }
        }
    }
}
