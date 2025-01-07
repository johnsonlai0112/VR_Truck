using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject setting;
    public GameObject levelSelect;

    [Header("Main Menu Buttons")]
    public Button startGameButton;
    public Button settingButton;
    public Button quitButton;

    [Header("Level Select Buttons")]
    public Button careerModeButton;
    public Button casualModeButton;
    public Button tutorialButton;

    public List<Button> backButtons;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu();

        //Hook events
        startGameButton.onClick.AddListener(LevelSelect);
        settingButton.onClick.AddListener(Setting);
        quitButton.onClick.AddListener(QuitGame);
        careerModeButton.onClick.AddListener(CareerMode);
        casualModeButton.onClick.AddListener(CasualMode);
        //tutorialButton.onClick.AddListener(Tutorial); 

        foreach (var item in backButtons)
        {
            item.onClick.AddListener(MainMenu);
        }
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        setting.SetActive(false);
    }
    public void Setting()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        setting.SetActive(true);
    }

    public void LevelSelect()
    {
        HideAll();
        levelSelect.SetActive(true);
    }

    public void CareerMode()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void CasualMode()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(2);
    }

    public void Tutorial()
    {
        HideAll();
        // TODO
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        setting.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
