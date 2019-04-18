using System.Collections;
using UnityEngine;

public class GameManager : _SigletonBehaviour<GameManager> {

    public PlayerController playerController;

    private int currentScore = 0;
    private int timer = 10;
    private int readyCountdown = 3;

    public bool isRunning = false;
    private bool isReadyToStart = false;

    public delegate void StartGameHandler();
    public delegate void StopGameHandler();
    public event StartGameHandler StartGameEvent;
    public event StopGameHandler StopGameEvent;

    public override void Initialize() {
        
    }

    void Update() {
        if(isReadyToStart && InputManager.isControllActivated())
            SetLevel();      
    }

    public void Start() {
        playerController = FindObjectOfType<PlayerController>();
        SetLevel();
    }

    public void SetLevel() {
        isReadyToStart = false;
        currentScore = 0;
        timer = 40;
        readyCountdown = 3;
        playerController.SetPosition(Vector3.zero);
        HUDManager.instance.SetText("ScoreLabel", "Score: " + currentScore);
        HUDManager.instance.SetText("TimeLabel", " 00:" + (timer < 10 ? "0" + timer : timer.ToString()));
        HUDManager.instance.Hidden("MessageLabel", false);
        StartCoroutine(TimeProcess());
    }

    private IEnumerator TimeProcess() {

        for(; ; ) {
            if(readyCountdown >= 0) {
                HUDManager.instance.SetText("MessageLabel", "Get Ready!\n" + readyCountdown--);
            }
            else {
                if(!isRunning) {
                    HUDManager.instance.Hidden("MessageLabel", true);
                    isRunning = true;
                    StartGameEvent();
                }

                HUDManager.instance.SetText("TimeLabel", " 00:" + (timer < 10 ? "0" + timer : timer.ToString()));

                if(timer-- <= 0) {
                    HUDManager.instance.SetText("MessageLabel", "Time is over!\n" + (Application.isMobilePlatform ? "Tap" : "Press any key") + " to continue");
                    HUDManager.instance.Hidden("MessageLabel", false);
                    isRunning = false;
                    isReadyToStart = true;
                    StopGameEvent();
                    break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public void IncreaseScore() {
        HUDManager.instance.SetText("ScoreLabel", "Score: " + ++currentScore);
    }

    public void SetCubesCounter(int quantity) {
        HUDManager.instance.SetText("CubeCounterLabel", "Cubes: " + quantity + " ");
    }

}