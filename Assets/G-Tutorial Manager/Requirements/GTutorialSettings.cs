using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GTutorialSettings : ScriptableObject
{
    [Header("Settings")]
    public float scanValidatorsDelayInMs = 5f;
    [Header("References")]
    public List<TutorialSectionGroup> AllTutorialGroups;

    public void GetAllTutorials()
    {
        AllTutorialGroups.Clear();

        var itemList = AssetDatabase.FindAssets("t:ScriptableObject");
            
        for (int i = 0; i < itemList.Length; i++)
        {
            var loadedTutorial = AssetDatabase.LoadAssetAtPath<TutorialSectionGroup>(AssetDatabase.GUIDToAssetPath(itemList[i]));
            if (loadedTutorial != null)
            {
                AllTutorialGroups.Add(loadedTutorial);
            }
        }
    }

    [CustomEditor(typeof(GTutorialSettings))]
    public class GTutorialSettingsEditor : Editor
    {
        private GTutorialSettings _tutorialSettings;

        private void OnEnable()
        {
            _tutorialSettings = target as GTutorialSettings;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Get All Tutorials"))
            {
                _tutorialSettings.GetAllTutorials();
            }
        }
    }
}
