using System.IO;
using UnityEditor;
using UnityEngine;

namespace BaseSource
{
    public static class PackageMenuItem
    {
        [MenuItem("BaseSource/Package/Open manifest.json")]
        public static void OpenManifestJsonFile()
        {
            var manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            manifestPath = Path.GetFullPath(manifestPath);

            if (File.Exists(manifestPath))
            {
                OpenFileInDefaultEditor(manifestPath);
            }
            else
            {
                Debug.LogError($"manifest.json file not found at {manifestPath}");
            }
        }

        [MenuItem("BaseSource/Package/Open Package Folder")]
        public static void OpenPackageFolderMenu()
        {
            var packageFolderPath = Path.Combine(Application.dataPath, "../Packages/");
            packageFolderPath = Path.GetFullPath(packageFolderPath);

            if (Directory.Exists(packageFolderPath))
            {
                EditorUtility.RevealInFinder(packageFolderPath);
            }
            else
            {
                Debug.LogError("Package folder not found!");
            }
        }

        public static void OpenFileInDefaultEditor(string filePath)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = filePath,
                UseShellExecute = true,
                Verb = "open"
            };
            process.Start();
        }
    }
}