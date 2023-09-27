using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TutorialManager>();
            }

            return _instance;
        }
    }
    
    private TutorialUIController _tutorialUIController;
    public TutorialUIController UI => _tutorialUIController;

    [HideInInspector] public List<TutorialSectionGroup> ValidatingTutorialGroups;
    [HideInInspector] public List<TutorialSectionGroup> ActiveTutorialGroups;

    [HideInInspector] public bool activeTutorialLoopIsRunning;

    public GTutorialSettings TutorialSettings;

    private void Awake()
    {
        ActiveTutorialGroups = new List<TutorialSectionGroup>();
        _tutorialUIController = GetComponentInChildren<TutorialUIController>();

        for (var i = 0; i < TutorialSettings.AllTutorialGroups.Count; i++)
        {
            var tutorialGroup = TutorialSettings.AllTutorialGroups[i];
            if (tutorialGroup.TutorialIsCompleted) continue;

            tutorialGroup = tutorialGroup.Clone();
            tutorialGroup.Init();

            if (tutorialGroup.triggerCondition == TutorialSectionGroup.TriggerCondition.Validator)
                ValidatingTutorialGroups.Add(tutorialGroup);
        }
    }

    private void Start()
    {
        //START VALIDATOR COROUTINE
        StartCoroutine(SectionGroupTriggerValidatorCoroutine());
    }

    public void StartTutorial(TutorialSectionGroup sectionGroup)
    {
        ActiveTutorialGroups.Add(sectionGroup);
        sectionGroup.OnTutorialStart();

        if (!activeTutorialLoopIsRunning)
            StartCoroutine(ActiveSectionGroupsUpdateCoroutine());
    }

    private IEnumerator ActiveSectionGroupsUpdateCoroutine()
    {
        while (ActiveTutorialGroups.Count != 0)
        {
            for (int i = 0; i < ActiveTutorialGroups.Count; i++)
            {
                ActiveTutorialGroups[i].Tick();
                
                if (ActiveTutorialGroups[i].EndCheck())
                {
                    ActiveTutorialGroups[i].Destroy();
                    ActiveTutorialGroups.RemoveAt(i--);
                }
            }

            yield return null;
        }
        
        activeTutorialLoopIsRunning = false;
    }
    
    private IEnumerator SectionGroupTriggerValidatorCoroutine()
    {
        var validatorDelay = new WaitForSecondsRealtime(TutorialSettings.scanValidatorsDelayInMs / 1000);
        
        while (ValidatingTutorialGroups.Count != 0)
        {
            for (int i = 0; i < ValidatingTutorialGroups.Count; i++)
            {
                if (ValidatingTutorialGroups[i].IsValid())
                {
                    StartTutorial(ValidatingTutorialGroups[i]);
                    ValidatingTutorialGroups.RemoveAt(i--);
                }
            }

            yield return validatorDelay;
        }
    }
}
 