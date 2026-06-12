using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnvironmentalChange : MonoBehaviour
{
    [Header("Skybox")]
    public Material targetSkybox;

    [Header("Fog")]
    public Color targetFogColor = Color.gray;
    public float targetFogDensity = 0.01f;

    private float originalFogStartDistance;
    public float targetFogStartDistance = 0f;

    [Header("Transition")]
    public float transitionDuration = 2f;

    private Material originalSkybox;
    private Color originalFogColor;
    private float originalFogDensity;

    private Coroutine transitionCoroutine;

    private void Start()
    {
        originalSkybox = RenderSettings.skybox;
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;
        originalFogStartDistance = RenderSettings.fogStartDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        StartTransition(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        StartTransition(false);
    }

    void StartTransition(bool enterZone)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(
            TransitionEnvironment(enterZone)
        );
    }

    IEnumerator TransitionEnvironment(bool enterZone)
    {
        Color startColor = RenderSettings.fogColor;
        float startDensity = RenderSettings.fogDensity;
        float startFogStartDistance = RenderSettings.fogStartDistance;

        Color endColor = enterZone ? targetFogColor : originalFogColor;

        float endDensity = enterZone ? targetFogDensity : originalFogDensity;
        float endFogStartDistance = enterZone ? targetFogStartDistance : originalFogStartDistance;

        float timer = 0f;

        bool skyboxChanged = false;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;

            float t = timer / transitionDuration;

            RenderSettings.fogColor =
                Color.Lerp(startColor, endColor, t);

            RenderSettings.fogDensity =
                Mathf.Lerp(startDensity, endDensity, t);

            RenderSettings.fogStartDistance = Mathf.Lerp(startFogStartDistance, endFogStartDistance, t);

            if (!skyboxChanged && t >= 0.5f)
            {
                RenderSettings.skybox =
                    enterZone ? targetSkybox : originalSkybox;

                RenderSettings.fogStartDistance = endFogStartDistance;

                DynamicGI.UpdateEnvironment();

                skyboxChanged = true;
            }

            yield return null;
        }

        RenderSettings.fogColor = endColor;
        RenderSettings.fogDensity = endDensity;

        transitionCoroutine = null;
    }
}