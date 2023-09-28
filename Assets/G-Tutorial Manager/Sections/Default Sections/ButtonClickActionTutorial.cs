using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickActionTutorial : TutorialSection
{
    [HideInInspector] public Button targetButton;
    [SerializeField] protected string buttonName;
    [SerializeField] protected string tutorialText;
    [Tooltip("Set Text alignment when button is glowed.")]
    [SerializeField] protected TextAlignmentOptions textAlignment = TextAlignmentOptions.Bottom;
    [SerializeField] protected bool glowButton;
    [SerializeField] protected bool pauseGame;

    protected bool _onClickButton;
    protected TutorialUIController _tutorialUIController;

    public override void OnTutorialStart()
    {
        _tutorialUIController = TutorialManager.Instance.UI;
        targetButton = GameObject.Find(buttonName)?.GetComponent<Button>();
        if (targetButton == null)
        {
            _onClickButton = true;
            return;
        }
        
        _onClickButton = false;
        
        SetupVisual();
        
        targetButton.onClick.AddListener(OnClickButton);

        if (pauseGame)
            Time.timeScale = 0;
    }

    protected void SetupVisual()
    {
        _tutorialUIController.SetHandActive(true)
            .SetHandPosition(targetButton.transform.position)
            .SetHandAnimation(TutorialUIController.HandAnimation.Click);

        if (glowButton)
        {
            _tutorialUIController.SetInfoActive(true).InfoGlowUI(targetButton.GetComponent<RectTransform>())
                .SetInfoText(tutorialText, textAlignment);
        }
        else
        {
            _tutorialUIController.SetHandText(tutorialText);
        }
    }

    public override void OnTutorialEnded()
    {
        if (pauseGame)
            Time.timeScale = 1;
        
        _tutorialUIController.SetHandActive(false);
        _tutorialUIController.SetInfoActive(false);
    }

    protected void OnClickButton()
    {
        targetButton.onClick.RemoveListener(OnClickButton);
        _onClickButton = true;
    }

    public override bool EndCheck()
    {
        return _onClickButton;
    }
}