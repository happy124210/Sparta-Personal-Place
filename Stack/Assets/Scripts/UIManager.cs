using UnityEngine;

public enum UIState
{
    Home,
    Game,
    Score
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance {get {return instance;}}
    
    private UIState currentState = UIState.Home;
    private HomeUI homeUI;
    private GameUI gameUI;
    private ScoreUI scoreUI;
    private TheStack theStack;

    private void Awake()
    {
        instance = this;
        theStack = FindObjectOfType<TheStack>();
        homeUI = GetComponentInChildren<HomeUI>();
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>();
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<ScoreUI>();
        scoreUI?.Init(this);
        
        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Home);
    }

    public void OnClickExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    
}


