using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeatReceiver : MonoBehaviour
{
    AudioSource audioSource;
    LineRenderer lr;
    public BeatDetection beatDetection; //add GO with the BeatDetection Script in here. Add BD Script to Main Camera
    // float cooldown = 0.4f;
    // float currentCooldown = 0f;

    IBeatObject[] beater;

    void Start()
    {
        beatDetection = GameObject.FindGameObjectWithTag("SoundController").GetComponent<BeatDetection>();

        beater = GetComponents<IBeatObject>();

        beatDetection.CallBackFunction += OnBeatDetected;
    }

    void OnBeatDetected(BeatDetection.EventInfo eventInfo)
    {
        for (int i = 0; i < beater.Length; i++)
        {
            beater[i]?.Beat();
        }
    }
}