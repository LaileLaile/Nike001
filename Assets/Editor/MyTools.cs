using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyTools
{
    [MenuItem("Tools/AssetBundle Tool")]
    static public void OpenPanelWizard()
    {
        EditorWindow.GetWindow<AssetBundleTool>(false, "AssetBundle", true);
    }
}
