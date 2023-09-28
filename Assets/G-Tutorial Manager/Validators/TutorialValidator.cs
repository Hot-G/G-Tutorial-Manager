using UnityEngine;

public abstract class TutorialValidator : ScriptableObject
{
    public abstract bool IsValid();
    
    public override string ToString()
    {
        return GetType().Name;
    }
}
