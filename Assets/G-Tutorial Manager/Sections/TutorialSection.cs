using UnityEngine;

public abstract class TutorialSection : ScriptableObject
{
    public abstract bool EndCheck();
    public virtual void OnTutorialStart(){}
    public virtual void OnTutorialEnded(){}
    public virtual void Tick(){}

    public override string ToString()
    {
        return GetType().Name;
    }
}