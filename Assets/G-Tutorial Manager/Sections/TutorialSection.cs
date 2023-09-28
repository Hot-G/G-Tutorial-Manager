using UnityEngine;

[System.Serializable]
public abstract class TutorialSection : ScriptableObject
{
    public virtual void OnTutorialStart(){}
    public virtual void OnTutorialEnded(){}
    public abstract bool EndCheck();
    public abstract void Tick();
}