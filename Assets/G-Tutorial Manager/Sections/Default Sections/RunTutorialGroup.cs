using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTutorialGroup : TutorialSection
{
    [SerializeField] private TutorialSectionGroup tutorialSectionGroup;

    public override void OnTutorialStart()
    {
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
