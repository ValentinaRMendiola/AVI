using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimalProximityAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true; // para que el sonido continúe mientras esté cerca
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        audioSource.Stop();
    }
}

