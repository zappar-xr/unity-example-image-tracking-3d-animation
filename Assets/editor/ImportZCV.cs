using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using Zappar.Editor;

public class ImportZCV
{

    private static AddRequest s_importRequest = null;
    private static ListRequest s_listRequest = null;

    public const string PackageId = "https://github.com/zappar-xr/universal-ar-unity.git";

    public static void Import()
    {
        Debug.Log("[Universal AR ]: Checking package lists ...");
        s_listRequest = UnityEditor.PackageManager.Client.List();
        EditorApplication.update += PackageProgress;
    }

    static void PackageProgress()
    {
        if (s_listRequest != null)
        {
            if (s_listRequest.IsCompleted)
            {
                if (s_listRequest.Status == StatusCode.Success)
                {
                    string pack = PackageId;
                    foreach (var p in s_listRequest.Result)
                    {
                        if (p.packageId.Contains(PackageId))
                        {
                            Debug.Log("[Universal AR ]: Reimporting package id: " + p.packageId);
                            pack = p.packageId;
                        }
                    }
                    s_importRequest = Client.Add(pack);
                    ImportPackageStarted(pack);
                    s_listRequest = null;
                    return;
                }
                else if (s_listRequest.Status >= StatusCode.Failure)
                {
                    Debug.Log("[Universal AR ]: Failed checking list. Error: " + s_listRequest.Error.message);
                }
                EditorApplication.update -= PackageProgress;
                s_listRequest = null;
            }
        }

        if(s_importRequest != null)
        {
            if (s_importRequest.IsCompleted)
            {
                if (s_importRequest.Status == StatusCode.Failure)
                {
                    ImportPackageFailed(s_importRequest.Result.packageId, s_importRequest.Error.message);
                }
                else if (s_importRequest.Status == StatusCode.Success)
                {
                    ImportPackageCompleted(s_importRequest.Result.packageId);
                }
                EditorApplication.update -= PackageProgress;
                s_importRequest = null;
            }
        }

        if(s_importRequest == null && s_listRequest==null)
        {
            EditorApplication.update -= PackageProgress;
        }
    }

    public static void UpdateProjectSettings()
    {
        ZapparMenu.ZapparPublishSettings();
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