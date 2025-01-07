using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject timerObject; // Reference to the timer GameObject
    Timer timer;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject leftRay;
    public GameObject rightRay;

    // Start is called before the first frame update
    void Start()
    {
        CheckAndSetTimer();
        WinCondition.reachGoal = false;
        timer = timerObject.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timeUp && !WinCondition.reachGoal) {
            timer.StopTimer();
            leftRay.SetActive(true);
            rightRay.SetActive(true);
            losePanel.SetActive(true);
            Debug.Log("Lose");
        }

        if (WinCondition.reachGoal && !timer.timeUp) {
            timer.StopTimer();
            leftRay.SetActive(true);
            rightRay.SetActive(true);
            winPanel.SetActive(true);
            Debug.Log("Win");
        }
    }

    public void CheckAndSetTimer() {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timerObject.SetActive(true);
        }
        else {
            timerObject.SetActive(false);
        }
    }
}
