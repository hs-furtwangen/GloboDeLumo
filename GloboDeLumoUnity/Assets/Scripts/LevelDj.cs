using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDj : MonoBehaviour
{
    public AudioClip[] Songs;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;

        StartSongForLevel(0);
    }

    public void StartSongForLevel(int i, float fadeTime = 2)
    {
        if (i >= 0 && i < Songs.Length && Songs[i] != null)
        {
            if (audioSource.isPlaying)
            {
                StartCoroutine(ChangeTrack(i, fadeTime));
            }
            else
            {
                audioSource.clip = Songs[i];
                audioSource.Play();
            }
        }
    }

    public void FadeOutCurrentSong(float fadeTime = 3)
    {
        StartCoroutine(FadeOut(fadeTime));
    }

    IEnumerator ChangeTrack(int i, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        audioSource.clip = Songs[i];
        audioSource.Play();
    }


    IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
