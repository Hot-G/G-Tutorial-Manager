using System.IO;
using UnityEngine;
using UnityEditor;
using System.CodeDom.Compiler;


public class CreateScriptEditor
{
    [MenuItem("Assets/Create/G Tutorial/New Tutorial Section")]
    static void CreateTutorialSection()
    {
        var win = ScriptableObject.CreateInstance<NameWindow>();
        
        var currentPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (currentPath == string.Empty)
            currentPath = "Assets";
        
        win.OnValidate = s =>
        {
            string[] asset = AssetDatabase.FindAssets("SampleTutorialSection");

            if (asset.Length > 0)
            {
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(asset[0]));

                string result = textAsset.text.Replace("{SAMPLENAME}", s);
                
                string targetPath = Application.dataPath.Replace("Assets", currentPath);
                targetPath += "/" + s + ".cs";
                File.WriteAllText(targetPath, result);
                AssetDatabase.Refresh();

                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(targetPath.Replace(Application.dataPath, "Assets"));
                EditorWindow.GetWindow(System.Type.GetType("UnityEditor.ProjectBrowser, UnityEditor"));
                EditorGUIUtility.PingObject(Selection.activeObject);
            }
            else
            {
                Debug.LogError("Couldn't find the sample tutorial section script file");
            }
        }; 

        win.Display("Create New Tutorial Section", "Tutorial Section Name");
    }
    
    [MenuItem("Assets/Create/G Tutorial/New Validator")]
    static void CreateValidator()
    {
        var win = ScriptableObject.CreateInstance<NameWindow>();
            
        var currentPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (currentPath == string.Empty)
            currentPath = "Assets";
            
        win.OnValidate = s =>
        {
            string[] asset = AssetDatabase.FindAssets("SampleValidator");

            if (asset.Length > 0)
            {
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(asset[0]));

                string result = textAsset.text.Replace("{SAMPLENAME}", s);
                    
                string targetPath = Application.dataPath.Replace("Assets", currentPath);
                targetPath += "/" + s + ".cs";
                File.WriteAllText(targetPath, result);
                AssetDatabase.Refresh();

                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(targetPath.Replace(Application.dataPath, "Assets"));
                EditorWindow.GetWindow(System.Type.GetType("UnityEditor.ProjectBrowser, UnityEditor"));
                EditorGUIUtility.PingObject(Selection.activeObject);
            }
            else
            {
                Debug.LogError("Couldn't find the sample tutorial section script file");
            }
        }; 

        win.Display("Create New Validator", "Validator Name");
    }
}


public class NameWindow : EditorWindow
{
    public System.Action<string> OnValidate;

    private string _outputName;
    private string _nameOutputText;

    CodeDomProvider _provider;
    
    public void Display(string windowTitle, string nameOutputText)
    {
        var pos = position;
        pos.size = new Vector2(400, 300);
        pos.position = new Vector2(Screen.width / 2, 400);
        position = pos;

        titleContent = new GUIContent(windowTitle);
        _nameOutputText = nameOutputText;

        _outputName = "";

        if (_provider == null)
            _provider = CodeDomProvider.CreateProvider("CSharp");
        
        ShowModalUtility();
    }

    private void OnGUI()
    {
        _outputName = EditorGUILayout.TextField(_nameOutputText, _outputName);

        bool validName = _provider.IsValidIdentifier(_outputName);
        
        EditorGUILayout.BeginHorizontal();

        GUI.enabled = validName;
        if (GUILayout.Button(validName ? "Create" : "Invalid Name"))
        {
            OnValidate(_outputName);
            Close();
        }

        GUI.enabled = true;
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
        
        EditorGUILayout.EndHorizontal();

        if (!validName)
        {
            EditorGUILayout.HelpBox("The name is not valid. It shouldn't contains space, start with a number or contains special character like ; or .", MessageType.Error);
        }
    }
}
