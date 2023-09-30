using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "G Tutorial/New Tutorial Section Group", fileName = "Tutorial Section Group", order = 0)]
public class TutorialSectionGroup : ScriptableObject
{
    private TutorialSection _currentTutorialSection;
    private int _currentSectionIndex;
    private bool _sectionIsCompleted;

    public string GroupName;
    public bool LoadLastSave = true;
    public TriggerCondition triggerCondition;
    public TutorialValidator Validator;
    
    public List<TutorialSection> TutorialSections;
    
    public enum TriggerCondition
    {
        Manual,
        Validator
    }
    
    public bool TutorialIsCompleted => PlayerPrefs.GetInt(GroupName) >= TutorialSections.Count;
    
    #region Constructors and Destructors

    public static TutorialSectionGroup New()
    {
        var newInstance = ScriptableObject.CreateInstance<TutorialSectionGroup>();
        newInstance.TutorialSections = new List<TutorialSection>();

        return newInstance;
    }

    public static TutorialSectionGroup New(List<TutorialSection> sectionGroup)
    {
        var newInstance = ScriptableObject.CreateInstance<TutorialSectionGroup>();
        newInstance.TutorialSections = sectionGroup;

        return newInstance;
    }

    public static TutorialSectionGroup New(TutorialSection section)
    {
        var newInstance = ScriptableObject.CreateInstance<TutorialSectionGroup>();
        newInstance.TutorialSections = new List<TutorialSection> { section };

        return newInstance;
    }

    public TutorialSectionGroup Clone()
    {
        var instance = Instantiate(this);
        if (triggerCondition == TriggerCondition.Validator)
            instance.Validator = Instantiate(instance.Validator);

        for (int i = 0; i < instance.TutorialSections.Count; i++)
        {
            instance.TutorialSections[i] = Instantiate(TutorialSections[i]);
        }

        return instance;
    }

    public void Destroy()
    {
        for (var i = 0; i < TutorialSections.Count; i++)
        {
            Destroy(TutorialSections[i]);
        }

        Destroy(this);
    }
    
    public void Add(TutorialSection newSection)
    {
        TutorialSections.Add(newSection);
    }
    
    #endregion

    public void Init()
    {
    }

    /// <summary>
    /// Check this Group is valid to run
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (triggerCondition == TriggerCondition.Validator)
            return Validator.IsValid();

        return false;
    }

    public void OnTutorialStart()
    {
        if (LoadLastSave)
            _currentSectionIndex = PlayerPrefs.GetInt(GroupName);

        _currentTutorialSection = TutorialSections[_currentSectionIndex];
        _currentTutorialSection.OnTutorialStart();
    }

    public bool EndCheck()
    {
        return _sectionIsCompleted;
    }

    public void Tick()
    {
        if (_currentTutorialSection == null) return;

        if (_currentTutorialSection.EndCheck())
        {
            _currentTutorialSection.OnTutorialEnded();
            _currentSectionIndex++;
            //SAVE INDEX
            PlayerPrefs.SetInt(GroupName, _currentSectionIndex);

            if (_currentSectionIndex >= TutorialSections.Count)
            {
                _sectionIsCompleted = true;
                return;
            }

            _currentTutorialSection = TutorialSections[_currentSectionIndex];
            _currentTutorialSection.OnTutorialStart();
        }

        _currentTutorialSection.Tick();
    }
}