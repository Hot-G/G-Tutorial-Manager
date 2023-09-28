using UnityEngine;

namespace GTutorialManager.Demo
{
    public class SecondTutorialValidator : PressKeyValidator
    {
        [SerializeField] private TutorialSectionGroup targetGroup;
        
        public override bool IsValid()
        {
            if (!targetGroup.TutorialIsCompleted) return false;
            return base.IsValid();
        }
    }   
}
