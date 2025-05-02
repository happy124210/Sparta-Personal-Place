using TMPro;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI bestComboText;
    TextMeshProUGUI bestScoreText;
    TextMeshProUGUI scoreText;
    
    Button restartButton;
    Button exitButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
        
        bestComboText = transform.Find("ScorePanel/BestComboText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("ScorePanel/BestScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScorePanel/ScoreText").GetComponent<TextMeshProUGUI>();
        
        restartButton = transform.Find("ScorePanel/RestartButton").GetComponent<Button>();
        exitButton = transform.Find("ScorePanel/ExitButton").GetComponent<Button>();
        
        restartButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    
    public void SetUI(int score, int bestCombo, int bestScore)
    {
        scoreText.text = score.ToString();
        bestComboText.text = bestCombo.ToString();
        bestScoreText.text = bestScore.ToString();
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
