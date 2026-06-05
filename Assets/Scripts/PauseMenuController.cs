using UnityEngine;
using Convai.Scripts.Runtime.Addons;

public class PauseMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Player")]
    [SerializeField] private ConvaiPlayerMovement player;

    private bool paused;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
                return;
            }

            if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Pausing game...");
        paused = true;

        Time.timeScale = 0f;

        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pausePanel.SetActive(true);

        settingsPanel.SetActive(false);
    }

    public void Resume()
    {
        Debug.Log("Resuming game...");
        pausePanel.SetActive(false);

        settingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.enabled = true;

        Time.timeScale = 1f;

        paused = false;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseSettings()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        SaveManager.Instance.SaveGame(player);

        SceneTransitionManager.Instance
            .LoadScene("MainMenu");
    }
}