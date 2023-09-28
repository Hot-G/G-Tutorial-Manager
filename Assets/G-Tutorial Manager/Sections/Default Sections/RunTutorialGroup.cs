using UnityEngine;

public class RunTutorialGroup : TutorialSection
{
    [SerializeField] private TutorialSectionGroup tutorialSectionGroup;

    public override void OnTutorialStart()
    {
        tutorialSectionGroup = tutorialSectionGroup.Clone();
        tutorialSectionGroup.OnTutorialStart();
    }

    public override bool EndCheck()
    {
        return tutorialSectionGroup.EndCheck();
    }

    public override void Tick()
    {
        tutorialSectionGroup.Tick();
    }
}
