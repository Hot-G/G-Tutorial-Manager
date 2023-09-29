using TMPro;
using UnityEngine;

public class ShowInfoTextTutorial : DelayBetweenTutorial
{
    [SerializeField] private string tutorialText;
    [SerializeField] protected TextAlignmentOptions textAlignment = TextAlignmentOptions.Bottom;
    [SerializeField] private bool showBlackBackground;

    public override void OnTutorialStart()
    {
        TutorialManager.Instance.UI.SetInfoActive(true, showBlackBackground)
            .SetInfoText(tutorialText, textAlignment);
    }

    public override void OnTutorialEnded()
    {
        TutorialManager.Instance.UI.SetInfoActive(false);
    }
}
