using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioPlaying : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f;
    public float maxVolume = 1f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        StartFade(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        StartFade(false);
    }

    void StartFade(bool fadeIn)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAudio(fadeIn));
    }

    IEnumerator FadeAudio(bool fadeIn)
    {
        if (fadeIn && !audioSource.isPlaying)
            audioSource.Play();

        float startVolume = audioSource.volume;
        float targetVolume = fadeIn ? maxVolume : 0f;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (!fadeIn)
            audioSource.Pause();

        fadeCoroutine = null;
    }
}
