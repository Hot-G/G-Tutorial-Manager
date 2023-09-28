using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class TutorialAssetPostProcessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        for (var index = 0; index < importedAssets.Length; index++)
        {
            var importedAsset = importedAssets[index];
 
            var asset = AssetDatabase.LoadAssetAtPath<Object>(importedAsset);
 
            if (asset.GetType() == typeof(TutorialSectionGroup))
            {
                Resources.Load<GTutorialSettings>("G Tutorial Settings").GetAllTutorials();
            }
        }
        
        for (var index = 0; index < deletedAssets.Length; index++)
        {
            var deletedAsset = deletedAssets[index];

            if (deletedAsset.Contains(".asset"))
            {
                Resources.Load<GTutorialSettings>("G Tutorial Settings").GetAllTutorials();
                break;
            }
        }
    }

    
}
