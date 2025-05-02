using UnityEngine;

public class ScoreUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
    }
}
