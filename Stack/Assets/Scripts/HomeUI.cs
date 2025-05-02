using UnityEngine;

public class HomeUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);

    }
}
