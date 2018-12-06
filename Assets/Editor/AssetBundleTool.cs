using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class AssetBundleTool : EditorWindow
{
    //打包平台
    private enum Platform{
        Windows=0,
        Android=1,
        iPhone=2
    }
    private Platform target;

    //打包对象
    private enum PackType { 
        GirlIcon=0,
        Image=1,
        Music=2,
        Widget=3,
        Prop=4,
        Role=5
    }
    private PackType type;

    private BuildTarget buildTarget;

    private bool isMulti;
    private string filePath;

    void OnGUI()
    {
        EditorGUIUtility.LookLikeControls(100,100);

        GUILayout.Space(10f);
        GUILayout.Label("Make your assetBundle with the following parameters:");
        GUILayout.Space(10f);

        EvangelTool.DrawSeparator();

        GUILayout.BeginHorizontal();
        target = (Platform)EditorGUILayout.EnumPopup("BuildTarget:", target);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        type = (PackType)EditorGUILayout.EnumPopup("PackType:", type);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        isMulti = EditorGUILayout.Toggle("Multi-Packages", isMulti);
        GUILayout.Label("If build Multi-Packages,Please check it");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Click To Build");
        bool pack = GUILayout.Button("Package", GUILayout.Width(120f));
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);

        EvangelTool.DrawHeader("You Select: " + Selection.objects.Length + " objects");
        EditorGUIUtility.LookLikeControls(50, 50);

        foreach (Object o in Selection.objects){
            EvangelTool.HighlightLine(new Color(0.6f, 0.6f, 0.6f));
            EditorGUILayout.LabelField(o.name, o.GetType().ToString());
        }

        EditorGUILayout.HelpBox("After the success of BuildAssetBundle," +
                                "They are Stored in the upper layer of the project file\n\n" +
                                "which named Dynamic_Asset.", MessageType.Info);
        if (pack) Package(target,type);
    }

    void Package(Platform target,PackType type)
    {
        if (Selection.objects.Length == 0) {
            EditorUtility.DisplayDialog("Warning", "You have not selected any object", "Confirm");
            return;
        }
        switch (target){
            case Platform.Windows:
                buildTarget = BuildTarget.StandaloneWindows; break;
            case Platform.Android:
                buildTarget = BuildTarget.Android; break;
            case Platform.iPhone:
                buildTarget = BuildTarget.iOS; break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
		}
        BuildAsset(Selection.activeObject.name + ".assetbundle");
        //switch (type) {
            //case PackType.GirlIcon:
            //    BuildAsset("GirlIcon.assetbundle"); break;
            //case PackType.Image:
            //    BuildAsset("Image.assetbundle"); break;
            //case PackType.Music:
            //    BuildAsset("Music.assetbundle"); break;
            //case PackType.Prop:
            //    BuildAsset("Prop.assetbundle"); break;
            //case PackType.Role:
            //    BuildAsset("Role.assetbundle"); break;
            //case PackType.Widget:
            //    BuildAsset("Widget.assetbundle"); break;
            //default:
            //    Debug.LogError("Unrecognized Option");
                //break;
        //}
    }

    void BuildAsset(string name) {
        Directory.CreateDirectory("Dynamic_Asset");
        filePath = Application.streamingAssetsPath + "/Bundle/";

        string path = filePath + name;


        if (BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path, BuildAssetBundleOptions.CollectDependencies, buildTarget))
        {
            Debug.Log("create assetBundle [" + path + "] BINGO!");
        }
        else
        {
            Debug.Log("create assetBundle [" + path + "] ERROR!!!");
        }
    }
    void OnSelectionChange() { Repaint(); }
}
