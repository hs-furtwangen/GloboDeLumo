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

    private GameObject[] Lights;
    public bool active;

    private float BaseIntensity = 10;
    private float BeatIntensityRange = 5;
    private float BeatIntensity1;
    private float BeatIntensity2;

    public void Shutdown()
    {

    }

    public void Startup()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        Lights = new GameObject[32];

        var angle = 360f / Lights.Length;
        var radius = 0.1f;

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


        }
    }

    public void Beat()
    {
        if (active)
        {
            if (BeatIntensity1 == BeatIntensityRange)
            {
                BeatIntensity1 = 0;
                BeatIntensity2 = BeatIntensityRange;
            }
            else
            {
                BeatIntensity1 = BeatIntensityRange;
                BeatIntensity2 = 0;
            }
        }
    }
}
