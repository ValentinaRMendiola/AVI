using UnityEngine;
using UnityEngine.UI;

public class SettingsUIInitializer : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambientSlider;

    private void Start()
    {
        musicSlider.value =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                1f
            );

        ambientSlider.value =
            PlayerPrefs.GetFloat(
                "AmbientVolume",
                1f
            );
    }
}