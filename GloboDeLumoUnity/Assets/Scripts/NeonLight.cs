using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonLight : MonoBehaviour
{

    private Light _light;
    private MeshRenderer _model;
    private AudioSource _startUpSound;

    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
        _model = GetComponentInChildren<MeshRenderer>();
        _startUpSound = GetComponent<AudioSource>();
        _model.material.DisableKeyword("_EMISSION");
        _light.enabled = false;
        
    }

    void Start()
    {
        StartCoroutine("StartUpSequence");
    }

    IEnumerator StartUpSequence()
    {
        yield return new WaitForSeconds(Random.Range(13.0f, 15.0f));

        _startUpSound.Play();

        yield return new WaitForSeconds(Random.Range(1, 2));


        for (var i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.025f, 0.87f));

            _model.material.EnableKeyword("_EMISSION");
            _light.enabled = true;

            yield return new WaitForSeconds(Random.Range(0.05f, 0.75f));

            _model.material.DisableKeyword("_EMISSION");
            _light.enabled = false;
        }

        _model.material.EnableKeyword("_EMISSION");
        _light.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
