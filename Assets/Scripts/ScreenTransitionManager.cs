using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
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
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        // Fade a negro
        yield return FadeOut();

        // Cargar escena
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(sceneName);

        // Esperar hasta que termine realmente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar frames extra
        yield return null;
        yield return null;
        yield return null;

        // Espera opcional extra
        yield return new WaitForSecondsRealtime(1f);

        // Fade desde negro
        yield return FadeIn();
    }

    public IEnumerator FadeIn()
    {
        float t = fadeDuration;

        Color c = fadeImage.color;

        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;

            c.a = t / fadeDuration;
            fadeImage.color = c;

            yield return null;
        }

        c.a = 0;
        fadeImage.color = c;

        fadeImage.raycastTarget = false;
    }

    public IEnumerator FadeOut()
    {
        fadeImage.raycastTarget = true;

        float t = 0;

        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;

            c.a = t / fadeDuration;
            fadeImage.color = c;

            yield return null;
        }

        c.a = 1;
        fadeImage.color = c;
    }
}