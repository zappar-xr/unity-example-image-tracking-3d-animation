using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using Zappar.Editor;

public class ImportZCV : EditorWindowSingleton<ImportZCV>
{

    private static UnityEditor.PackageManager.Requests.AddRequest s_importRequest = null;

    [MenuItem("Window/Zappar - UAR package")]
    private static void ShowWindow()
    {
        ImportZCV window = GetWindow<ImportZCV>();
        window.titleContent = new GUIContent("Zappar - UAR package");
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Import ZCV package")) Import();
    }

    public static void Import()
    {
        Debug.Log("Importing UAR package...");

        string packageId = "https://github.com/zappar-xr/universal-ar-unity.git";
        s_importRequest = UnityEditor.PackageManager.Client.Add(packageId);
        EditorCoroutineUtility.StartCoroutine(CheckImportStatus(packageId), Instance);
    }

    public static void UpdateProjectSettings()
    {
        ZapparMenu.ZapparPublishSettings();
    }

    public static IEnumerator CheckImportStatus(string package)
    {
        if (s_importRequest == null)
        {
            ImportPackageFailed(package, "failed to request add package");
            yield break;
        }

        ImportPackageStarted(package);

        while (s_importRequest.Status == UnityEditor.PackageManager.StatusCode.InProgress)
        {
            yield return null;
        }

        if (s_importRequest.Status == UnityEditor.PackageManager.StatusCode.Failure)
        {
            ImportPackageFailed(package, s_importRequest.Error.message);
            yield break;
        }

        if (s_importRequest.Status == UnityEditor.PackageManager.StatusCode.Success)
        {
            ImportPackageCompleted(package);
        }
        yield break;
    }

    static void ImportPackageCompleted(string packageName)
    {
        Debug.Log("[Universal AR ]: Imported sucessfully " + packageName);
        s_importRequest = null;
    }

    static void ImportPackageStarted(string packageName)
    {
        Debug.Log("[Universal AR ]: Importing " + packageName);
    }

    static void ImportPackageFailed(string packageName, string errorMessage)
    {
        Debug.Log("[Universal AR ]: Import failed " + packageName);
        Debug.Log(errorMessage.ToString());
        s_importRequest = null;
    }

    static void ImportPackageCancelled(string packageName)
    {
       Debug.Log("[Universal AR ]: Import cancelled " + packageName);
    }

}