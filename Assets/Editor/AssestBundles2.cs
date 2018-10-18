#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
public class AssestBundles2{
  [MenuItem("Assets/Build AssetBundles")]
static void BuildAllAssestBundles()
{
  string dir ="AssetBundles";
if(Directory.Exists(dir)==false)
        Directory.CreateDirectory(dir);
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64); 

    }

}