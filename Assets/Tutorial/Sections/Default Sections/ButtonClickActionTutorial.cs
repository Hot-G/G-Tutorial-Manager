using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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
    
    private float _animationTime;
    private const float MaxAnimTime = 1f;

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
        _tutorialUIController.SetHandActive(true).SetHandPosition(targetButton.transform.position);

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

    public override void Tick()
    {
        _tutorialUIController.HandAnimateClick(Vector3.one * 1.2f, _animationTime / MaxAnimTime);

        _animationTime += Time.unscaledDeltaTime;
        if (_animationTime >= MaxAnimTime) _animationTime = 0;
    }
}