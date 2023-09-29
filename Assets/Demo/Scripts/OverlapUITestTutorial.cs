using UnityEngine;

namespace GTutorialManager.Demo
{
    public class OverlapUITestTutorial : DelayBetweenTutorial
    {
        [SerializeField] private string tutorialText;

        public override void OnTutorialStart()
        {
            Time.timeScale = 0f;

            var tutorialUIController = FindObjectOfType<Demo1Controller>();

            TutorialManager.Instance.UI.SetInfoActive(true)
                .SetInfoText(tutorialText)
                .SetInfoMaskPosition(tutorialUIController.healthSlider.transform.position)
                .SetInfoMaskSize(new Vector2(600, 600));
        }

        public override void OnTutorialEnded()
        {
            Time.timeScale = 1f;

            TutorialManager.Instance.UI.SetInfoActive(false);
        }
    }   
}