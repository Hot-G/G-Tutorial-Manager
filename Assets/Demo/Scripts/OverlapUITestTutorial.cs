using UnityEngine;

namespace GTutorialManager.Demo
{
    public class OverlapUITestTutorial : TutorialSection
    {
        private bool _timeIsOver;
        private float _currentTime;
    
        [SerializeField] private float delay = 1f;
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

        public override bool EndCheck()
        {
            return _timeIsOver;
        }

        public override void Tick()
        {
            _currentTime += Time.unscaledDeltaTime;
            if (_currentTime < delay) return;
            _timeIsOver = true;
        }
    }   
}