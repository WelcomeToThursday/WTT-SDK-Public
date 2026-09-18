using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ClearAssetBundleLabels : EditorWindow
{
    private string folderPath = "Assets";

    private static readonly string[] protectedFolders =
    {
        "Animation & StatiData template",
        "Common Texture Library",
        "Content",
        "Cubemaps",
        "Default",
        "Editor",
        "Packages",
        "Physics",
        "Plugins",
        "Scope Mesh Test",
        "ScriptPresets",
        "Scripts",
        "Shader Assets",
        "Standard Assets",
        "Systems",
        "Tools"
    };

    [MenuItem("Custom Windows/Groovey/Tools/Clear Asset Bundle Names")]
    public static void ShowWindow()
    {
        GetWindow<ClearAssetBundleLabels>("Clear Asset Bundle Names");
    }

    private void OnGUI()
    {
        GUILayout.Label("Clear Asset Bundle Names Tool", EditorStyles.boldLabel);
        GUILayout.Space(10);

        EditorGUILayout.LabelField("Selected Folder:", folderPath);

        if (GUILayout.Button("Select Folder"))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");

            if (!string.IsNullOrEmpty(selectedPath))
            {
                if (TryGetValidFolder(selectedPath, out string assetFolder, out string error))
                {
                    folderPath = assetFolder;
                }
                else
                {
                    EditorUtility.DisplayDialog("Invalid Folder", error, "OK");
                }
            }
        }

        GUILayout.Space(10);


        if (GUILayout.Button("Clear Asset Bundle Names"))
        {
            if (EditorUtility.DisplayDialog("Confirm", $"This will clear Asset Bundle names in {folderPath}. Proceed?", "Yes", "No"))
            {
                ClearAssetBundleNamesInFolder(folderPath);
            }
        }
    }

    public static void ClearAssetBundleNamesInFolder(string path)
    {
        if (!TryGetValidFolder(path, out string assetFolder, out string error))
        {
            EditorUtility.DisplayDialog("Invalid Folder", error, "OK");
            return;
        }

        path = assetFolder;
        string[] assetPaths = AssetDatabase.FindAssets("", new[] { path });

        int clearedCount = 0;

        foreach (string assetGUID in assetPaths)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);

            string containingFolder = AssetDatabase.IsValidFolder(assetPath)
                ? assetPath
                : Path.GetDirectoryName(assetPath)?.Replace('\\', '/');
            if (string.IsNullOrEmpty(containingFolder) || IsProtectedFolder(containingFolder))
            {
                continue;
            }

            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null && !string.IsNullOrEmpty(importer.assetBundleName))
            {
                importer.assetBundleName = string.Empty;
                clearedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Done", $"Cleared Asset Bundle names for {clearedCount} assets in {path}.", "OK");
    }

    private static bool TryGetValidFolder(string path, out string assetFolder, out string error)
    {
        assetFolder = null;
        error = "Please select an existing, unprotected subfolder inside the Assets directory.";
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        string assetsRoot = Path.GetFullPath(Application.dataPath).Replace('\\', '/').TrimEnd('/');
        string fullPath;
        
        try
        {
            string projectRoot = Path.GetDirectoryName(assetsRoot);
            fullPath = Path.GetFullPath(Path.Combine(projectRoot, path)).Replace('\\', '/').TrimEnd('/');
        }
        catch (Exception exception) when (exception is ArgumentException or NotSupportedException or IOException)
        {
            return false;
        }

        if (string.Equals(fullPath, assetsRoot, StringComparison.OrdinalIgnoreCase))
        {
            throw new ProtectedFolderException("Assets");
        }

        if (!fullPath.StartsWith(assetsRoot + "/", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        assetFolder = "Assets" + fullPath.Substring(assetsRoot.Length);

        if (!IsProtectedFolder(assetFolder))
        {
            return AssetDatabase.IsValidFolder(assetFolder);
        }
        
        throw new ProtectedFolderException(assetFolder);

    }

    private static bool IsProtectedFolder(string assetFolder)
    {
        string[] segments = assetFolder.Split('/');
        for (int i = 1; i < segments.Length; i++)
        {
            foreach (string protectedFolder in protectedFolders)
            {
                if (string.Equals(segments[i], protectedFolder, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private class ProtectedFolderException : Exception
    {
        public ProtectedFolderException(string folder) : base($"Folder '{folder}' is protected and cannot be modified.") { }
    }
}
