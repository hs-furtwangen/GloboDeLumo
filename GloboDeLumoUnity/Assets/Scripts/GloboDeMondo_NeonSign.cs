using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GloboDeMondo_NeonSign : MonoBehaviour
{
    private List<MeshRenderer> GloboMeshs;
    private List<MeshRenderer> DeMeshs;
    private List<MeshRenderer> LumoMeshs;

    private List<Light> GloboLights;
    private List<Light> DeLights;
    private List<Light> LumoLights;

    private MeshRenderer LumoO;
    private Light LumoRightLight;

    private AudioSource TurnOnSound;
    private AudioSource NeonSound;

    void TriggerGlobo(bool active)
    {
        StartCoroutine("GloboCoroutine", active);
    }

    IEnumerator GloboCoroutine(bool active)
    {
        foreach (var x in GloboMeshs)
        {

            yield return new WaitForSeconds(Random.Range(0.0f, 0.2f));


            if (x.name.Contains("G") || x.name.Contains("L"))
            {
                GloboLights.Find(x => x.name.Contains("GloboLight_Left")).enabled = active;
                if (active) x.material.EnableKeyword("_EMISSION");
                else x.material.DisableKeyword("_EMISSION");
            }

            if (x.name.Contains("B") || x.name.Contains("SecondO"))
            {
                GloboLights.Find(x => x.name.Contains("GloboLight_Right")).enabled = active;
                if (active) x.material.EnableKeyword("_EMISSION");
                else x.material.DisableKeyword("_EMISSION");
            }

            yield return new WaitForSeconds(Random.Range(0.0f, 0.2f));

            if (x.name.Contains("FirstO"))
            {
                GloboLights.Find(x => x.name.Contains("GloboLight_Middle")).enabled = active;
                if (active) x.material.EnableKeyword("_EMISSION");
                else x.material.DisableKeyword("_EMISSION");
            }

            yield return new WaitForSeconds(Random.Range(0.0f, 0.2f));


        }
    }

    private void TriggerDe(bool active)
    {
        DeLights.ForEach(x => x.enabled = active);
        DeMeshs.ForEach(x =>
        {
            if (active) x.material.EnableKeyword("_EMISSION");
            else x.material.DisableKeyword("_EMISSION");
        });
    }

    private void TriggerLumo(bool active)
    {
        LumoLights.ForEach(x => x.enabled = active);
        LumoMeshs.ForEach(x =>
        {
            if (active) x.material.EnableKeyword("_EMISSION");
            else x.material.DisableKeyword("_EMISSION");
        });
    }

    void Awake()
    {
        var globoGrp = transform.Find("Globo");
        var deGrp = transform.Find("De");
        var lumoGrp = transform.Find("Lumo");

        GloboMeshs = globoGrp.Find("GloboMesh").GetComponentsInChildren<MeshRenderer>().ToList();
        DeMeshs = deGrp.Find("DeMesh").GetComponentsInChildren<MeshRenderer>().ToList();
        LumoMeshs = lumoGrp.Find("LumoMesh").GetComponentsInChildren<MeshRenderer>().ToList();

        GloboMeshs.ForEach(x => x.material.DisableKeyword("_EMISSION"));
        DeMeshs.ForEach(x => x.material.DisableKeyword("_EMISSION"));
        LumoMeshs.ForEach(x => x.material.DisableKeyword("_EMISSION"));

        GloboLights = globoGrp.GetComponentsInChildren<Light>().ToList();
        DeLights = deGrp.GetComponentsInChildren<Light>().ToList();
        LumoLights = lumoGrp.GetComponentsInChildren<Light>().ToList();

        GloboLights.ForEach(x => x.enabled = false);
        DeLights.ForEach(x => x.enabled = false);
        LumoLights.ForEach(x => x.enabled = false);

        LumoRightLight = LumoLights.Where(x => x.name.Contains("LumoLight_Right")).FirstOrDefault();
        LumoO = LumoMeshs.Where(x => x.name.Contains("O")).FirstOrDefault();

        TurnOnSound = transform.Find("TurnOnSound").GetComponent<AudioSource>();
        NeonSound = transform.Find("NeonSound").GetComponent<AudioSource>();
    }

    private bool _startupComplete;

    void Start()
    {
        _ = StartCoroutine("StartUpRoutine");
    }

    IEnumerator StartUpRoutine()
    {
        yield return new WaitForSeconds(2);

        TurnOnSound.Play();

        yield return new WaitForSeconds(1);

        TriggerDe(true);
        for (var i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.25f));
            TriggerDe(false);
            yield return new WaitForSeconds(Random.Range(0, 0.75f));
            TriggerDe(true);
        }

        TurnOnSound.Play();

        NeonSound.Play();

        TriggerDe(true);
        for (var i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.6f, 0.35f));
            TriggerGlobo(false);
            yield return new WaitForSeconds(Random.Range(0.6f, 0.85f));
            TriggerGlobo(true);
        }

       

        TriggerLumo(true);
        for (var i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.25f));
            TriggerLumo(false);
            yield return new WaitForSeconds(Random.Range(0, 0.75f));
            TriggerLumo(true);
        }

        yield return new WaitForSeconds(1);

        _startupComplete = true;
        TurnOnSound.Stop();
    }

    IEnumerator FlickerLumoO()
    {
        LumoRightLight.enabled = false;
        LumoO.material.DisableKeyword("_EMISSION");

        yield return new WaitForSeconds(Random.Range(0.06f, 0.87f));

        LumoO.material.EnableKeyword("_EMISSION");
        LumoRightLight.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startupComplete)
            return;

        if (Random.Range(1, 10000) > 9990)
        {
            StartCoroutine("FlickerLumoO");
        }

    }


}
