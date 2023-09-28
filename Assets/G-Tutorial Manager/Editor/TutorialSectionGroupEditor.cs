#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TutorialSectionGroup))]
public class TutorialSectionGroupEditor : Editor
{
    private SerializedProperty _groupNameProperty;
    private SerializedProperty _loadLastSaveProperty;
    private SerializedProperty _sectionListProperty;
    private SerializedProperty _sectionTriggerConditionProperty;
    private SerializedProperty _sectionValidatorProperty;

    private List<string> _availableValidators;
    private List<string> _availableTutorials;

    private GUIStyle _titleStyle;
    private Editor _ed = null;
    private int _enumChoice = 0;

    private void OnEnable()
    {
        _groupNameProperty = serializedObject.FindProperty(nameof(TutorialSectionGroup.GroupName));
        _loadLastSaveProperty = serializedObject.FindProperty(nameof(TutorialSectionGroup.LoadLastSave));
        _sectionTriggerConditionProperty = serializedObject.FindProperty(nameof(TutorialSectionGroup.triggerCondition));
        _sectionValidatorProperty = serializedObject.FindProperty(nameof(TutorialSectionGroup.Validator));
        _sectionListProperty = serializedObject.FindProperty(nameof(TutorialSectionGroup.TutorialSections));

        _availableTutorials = GetSelectedAssembliesName(typeof(TutorialSection)).ToList();

        _availableValidators = GetSelectedAssembliesName(typeof(TutorialValidator)).ToList();

        _titleStyle = new GUIStyle
        {
            normal = new GUIStyleState
            {
                textColor = Color.white
            },
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            fontSize = 13
        };
    }

    public override void OnInspectorGUI()
    {
        _titleStyle.normal.background = MakeTex(1, 1, Color.gray);
        _titleStyle.fontSize = 15;
        EditorGUILayout.LabelField("Settings", _titleStyle);

        EditorGUILayout.PropertyField(_groupNameProperty);
        EditorGUILayout.PropertyField(_loadLastSaveProperty);
        EditorGUILayout.PropertyField(_sectionTriggerConditionProperty);
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// SELECT VALIDATOR
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if ((TutorialSectionGroup.TriggerCondition)_sectionTriggerConditionProperty.enumValueIndex ==
            TutorialSectionGroup.TriggerCondition.Validator)
        {
            var currentValidatorIndex = _sectionValidatorProperty.objectReferenceValue == null ?
                -1 : _availableValidators.IndexOf(_sectionValidatorProperty.objectReferenceValue.ToString());
            _enumChoice =
                EditorGUILayout.Popup("Select Validator", currentValidatorIndex, _availableValidators.ToArray());

            if (_enumChoice != currentValidatorIndex)
            {
                if (_sectionValidatorProperty.objectReferenceValue != null) //DESTROY OLD VALIDATOR
                {
                    DestroyImmediate(_sectionValidatorProperty.objectReferenceValue, true);
                }
                
                var newInstance = ScriptableObject.CreateInstance(_availableValidators[_enumChoice]);
                newInstance.name = _availableValidators[_enumChoice];
                AssetDatabase.AddObjectToAsset(newInstance, target);

                _sectionValidatorProperty.objectReferenceValue = newInstance;
            }

            if (_sectionValidatorProperty.objectReferenceValue != null)
            {
                Editor.CreateCachedEditor(_sectionValidatorProperty.objectReferenceValue, null, ref _ed);
                _ed.OnInspectorGUI();
            }
        }
        else if (_sectionValidatorProperty.objectReferenceValue != null) //DESTROY IF CHANGE TRIGGER CONDITION
        {
            DestroyImmediate(_sectionValidatorProperty.objectReferenceValue, true);
            _sectionValidatorProperty.objectReferenceValue = null;
        }

        EditorGUILayout.Space(20);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// SECTIONS PART
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        EditorGUILayout.LabelField("Sections", _titleStyle);

        _titleStyle.normal.background = MakeTex(1, 1, Color.black);
        _titleStyle.fontSize = 13;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ADD NEW TUTORIAL SECTION
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        _enumChoice = EditorGUILayout.Popup("Add New Tutorial Section", -1, _availableTutorials.ToArray());
        if (_enumChoice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(_availableTutorials[_enumChoice]);
            newInstance.name = _availableTutorials[_enumChoice];
            AssetDatabase.AddObjectToAsset(newInstance, target);

            _sectionListProperty.InsertArrayElementAtIndex(_sectionListProperty.arraySize);
            _sectionListProperty.GetArrayElementAtIndex(_sectionListProperty.arraySize - 1).objectReferenceValue =
                newInstance;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// DRAW SECTION EDITORS
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        int toDelete = -1;

        for (int i = 0; i < _sectionListProperty.arraySize; ++i)
        {
            var item = _sectionListProperty.GetArrayElementAtIndex(i);

            //Title
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(item.objectReferenceValue.ToString(), _titleStyle);
            EditorGUILayout.Space();
            //TITLE END

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref _ed);

            _ed.OnInspectorGUI();

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("↑", GUILayout.Width(32)))
            {
                if (i != 0)
                {
                    var tempElement = _sectionListProperty.GetArrayElementAtIndex(i).objectReferenceValue;
                    _sectionListProperty.GetArrayElementAtIndex(i).objectReferenceValue =
                        _sectionListProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue;
                    _sectionListProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue = tempElement;
                }
            }

            if (GUILayout.Button("↓", GUILayout.Width(32)))
            {
                if (i != _sectionListProperty.arraySize - 1)
                {
                    var tempElement = _sectionListProperty.GetArrayElementAtIndex(i).objectReferenceValue;
                    _sectionListProperty.GetArrayElementAtIndex(i).objectReferenceValue =
                        _sectionListProperty.GetArrayElementAtIndex(i + 1).objectReferenceValue;
                    _sectionListProperty.GetArrayElementAtIndex(i + 1).objectReferenceValue = tempElement;
                }
            }

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }

            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = _sectionListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            _sectionListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    private IEnumerable<Type> GetSelectedAssemblies(Type type)
    {
        return System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(type)
                        && x.GetConstructors().Any(info => info.GetParameters().Length == 0));
    }

    private IEnumerable<string> GetSelectedAssembliesName(Type type)
    {
        return GetSelectedAssemblies(type).Select(t => t.Name);
    }
}

#endif

