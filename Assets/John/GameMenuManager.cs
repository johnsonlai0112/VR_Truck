using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{

    public GameObject lever;
    public GameObject wheel;

    [Header("UI Pages")]
    public GameObject canvas;
    public GameObject pauseMenu;
    public GameObject setting;
    public GameObject win;
    public GameObject lose;

    [Header("Pause Menu Buttons")]
    public Button resumeButton;
    public List<Button> restartButton;
    public Button settingButton;
    public List<Button> backToMainButton;

    public List<Button> backButtons;

    public InputActionProperty showButton;
    public bool isPause = false;
    public bool showWinLosePanel = false;

    public GameObject leftRay;
    public GameObject rightRay;

    void Start()
    {
        leftRay.SetActive(false);
        rightRay.SetActive(false);

        //Hook events
        resumeButton.onClick.AddListener(Resume);
        settingButton.onClick.AddListener(Setting);

        foreach (var item in restartButton)
        {
            item.onClick.AddListener(Restart);
        }

        foreach (var item in backToMainButton)
        {
            item.onClick.AddListener(MainMenu);
        }

        foreach (var item in backButtons)
        {
            item.onClick.AddListener(Pause);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!win.activeInHierarchy || !lose.activeInHierarchy)
        {
            if (showButton.action.WasPressedThisFrame())
            {
                leftRay.SetActive(true);
                rightRay.SetActive(true);

                isPause = !isPause;
                if (isPause)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }

            }
        }
        else {
            PauseAll();
        }
        
        
    }

    void Pause()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
        pauseMenu.SetActive(true);
        lever.SetActive(false);
        wheel.SetActive(false);
        setting.SetActive(false);
    }

    void Resume()
    {
        leftRay.SetActive(false);
        rightRay.SetActive(false);

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        lever.SetActive(true);
        wheel.SetActive(true);
        canvas.SetActive(false);
    }

    public void Setting()
    {
        pauseMenu.SetActive(false);
        setting.SetActive(true);
    }

    void Restart()
    {
        HideAll();
        Time.timeScale = 1f; // Ensure time scale is reset to normal
        SceneTransitionManager.singleton.GoToSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }   

    void MainMenu() {
        HideAll();
        Time.timeScale = 1f; // Ensure time scale is reset to normal
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        pauseMenu.SetActive(false);
        setting.SetActive(false);
        canvas.SetActive(false);
    }

    public void PauseAll() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        setting.SetActive(false);
    }
}
