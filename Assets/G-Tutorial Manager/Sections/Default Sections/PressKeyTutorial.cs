using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressKeyTutorial : TutorialSection
{
    [SerializeField] private string tutorialText;
    [SerializeField] protected TextAlignmentOptions textAlignment = TextAlignmentOptions.Bottom;
    [SerializeField] private List<KeyCode> keys = new List<KeyCode>();

    public override void OnTutorialStart()
    {
        TutorialManager.Instance.UI.SetInfoActive(true, false)
            .SetInfoText(tutorialText);
    }

    public override void OnTutorialEnded()
    {
        TutorialManager.Instance.UI.SetInfoActive(false);
    }

    public override bool EndCheck()
    {
        return keys.Count == 0;
    }

    public override void Tick()
    {
        if (!Input.anyKey) return;

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i]))
                keys.RemoveAt(i--);
        }
    }
}
