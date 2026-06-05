using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance;

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat(
            "MusicVolume",
            Mathf.Log10(value) * 20
        );

        PlayerPrefs.SetFloat(
            "MusicVolume",
            value
        );

        PlayerPrefs.Save();
    }

    public void SetAmbientVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat(
            "AmbientVolume",
            Mathf.Log10(value) * 20
        );

        PlayerPrefs.SetFloat(
            "AmbientVolume",
            value
        );

        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        float music =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                1f
            );

        float ambient =
            PlayerPrefs.GetFloat(
                "AmbientVolume",
                1f
            );

        SetMusicVolume(music);
        SetAmbientVolume(ambient);
    }
}