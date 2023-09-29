using UnityEngine;

public class DelayBetweenTutorial : TutorialSection
{
    private bool _timeIsOver;
    private float _currentTime;
    
    [SerializeField] private float delay = 1f;

    public DelayBetweenTutorial()
    {
    }

    public DelayBetweenTutorial(float delay)
    {
        this.delay = delay;
    }
    
    public override bool EndCheck()
    {
        return _timeIsOver;
    }

    public override void Tick()
    {
        _currentTime += Time.unscaledDeltaTime;
        if (_currentTime < delay) return;
        _timeIsOver = true;
    }
}