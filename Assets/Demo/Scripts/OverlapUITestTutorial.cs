using UnityEngine;

namespace GTutorialManager.Demo
{
    public class OverlapUITestTutorial : DelayBetweenTutorial
    {
        [SerializeField] private string tutorialText;

        public override void OnTutorialStart()
        {
            Time.timeScale = 0f;

            TutorialManager.Instance.UI.SetInfoActive(true)
                .SetInfoText(tutorialText)
                .SetInfoMaskSize(new Vector2(300, 300));
        }

        public override void OnTutorialEnded()
        {
            Time.timeScale = 1f;

            TutorialManager.Instance.UI.SetInfoActive(false);
        }
    }   
}