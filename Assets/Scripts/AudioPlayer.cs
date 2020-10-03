using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource SFXaudioSource;
    public static AudioPlayer Instance { get; private set; }

    public static float currentPitch;

    public AudioClip audioClip;
    public AudioClip buttonClick;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentPitch = 1f;
        SetMusicVolume(.6f);
    }

    public void SetMusicDefaults()
    {
        SetMusicPitch(1);
        SetMusicVolume(0.6f);

        PlaySFX(buttonClick);
    }

    public void SetMusicPitch(float pitch)
    {
        StopCoroutine(lerp(0,0));
        StartCoroutine(lerp(pitch, 1));
    }

    public void SetMusicVolume(float volume)
    {

        StopCoroutine(lerp(0, 0));
        StartCoroutine(lerp(volume, 0));
    }

    IEnumerator lerp(float n, int option)
    {
        if (option == 0)
        {
            while(Mathf.Abs(audioSource.volume - n) >= 0.015f)
            {
                if (audioSource.volume > n)
                    audioSource.volume -= 0.01f;
                else if (audioSource.volume < n)
                    audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (option == 1)
        {
            while (Mathf.Abs(audioSource.pitch - n) >= 0.015f)
            {
                if (audioSource.pitch > n)
                    audioSource.pitch -= 0.01f;
                else if (audioSource.pitch < n)
                    audioSource.pitch += 0.01f;
                yield return new WaitForSeconds(0.0025f);
            }
        }
    }

    public void PlayCorrectAudio(AudioClip audioClip)
    {
        SFXaudioSource.volume = .25f;
        SFXaudioSource.Stop();
        SFXaudioSource.pitch = currentPitch;
        SFXaudioSource.clip = audioClip;
        SFXaudioSource.Play();
        currentPitch *= 1.02f;
    }
    public void PlaySFX(AudioClip audioClip)
    {
        SFXaudioSource.volume = .25f;
        SFXaudioSource.Stop();
        SFXaudioSource.pitch = 1;
        SFXaudioSource.clip = audioClip;
        SFXaudioSource.Play();
    }


    public void StopAudio()
    {
        audioSource.Stop();
    }
}
