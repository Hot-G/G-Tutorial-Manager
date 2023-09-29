using UnityEngine;

public class DragHandTutorial : DelayBetweenTutorial
{
    private TutorialUIController _tutorialUiController;
    private float _animationTime;

    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private string tutorialText;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField]private float animationDelay;
    
    public override void OnTutorialStart()
    {
        _tutorialUiController = TutorialManager.Instance.UI;
        
        _tutorialUiController.SetHandActive(true)
            .SetHandText(tutorialText);

        tutorialObject = Instantiate(tutorialObject);

        startPosition = tutorialObject.transform.GetChild(0).position;
        endPosition = tutorialObject.transform.GetChild(1).position;
    }

    public override void OnTutorialEnded()
    {
        _tutorialUiController.SetHandActive(false);
        
        Destroy(tutorialObject);
    }

    public override void Tick()
    {
        _tutorialUiController.HandAnimateMoveTwoPoints(startPosition, endPosition, 
            _animationTime / animationDelay);
        _animationTime = (_animationTime + Time.unscaledDeltaTime) % animationDelay;
        
        base.Tick();
    }
}
