using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    private Button startButton;
    private Button exitButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager manager)
    {
        Debug.Log("HomeUI.Init");
        base.Init(manager);
        Debug.Log($"startButton is {(startButton == null ? "NULL" : "OK")}");
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    private void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
