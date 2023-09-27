using System;

public class ActionBetweenTutorial : TutorialSection
{
    private Action _action;

    public ActionBetweenTutorial(Action action)
    {
        _action = action;
    }

    public override void OnTutorialStart()
    {
        _action.Invoke();
    }

    public override bool EndCheck()
    {
        return true;
    }

    public override void Tick()
    {

    }
}