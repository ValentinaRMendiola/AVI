using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainPanel;

    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.interactable =
            SaveManager.Instance.SaveExists();
    }

    public void StartGame()
    {
        SceneTransitionManager.Instance
            .LoadScene("mainScene");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SaveLoader.LoadFromSave = true;

        SaveData data =
            SaveManager.Instance.LoadGame();

        SceneTransitionManager.Instance
            .LoadScene(data.sceneName);
    }
}