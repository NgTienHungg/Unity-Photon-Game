using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BaseSource.Editor
{
    [CreateAssetMenu(fileName = "UnityPackageImporter", menuName = "Base Source/Unity Package Importer")]
    public class UnityPackageImporter : ScriptableObject
    {
        public List<Object> packageFiles;

        [ContextMenu("Import Packages")]
        public void ImportPackages()
        {
            foreach (var package in packageFiles)
            {
                if (package != null)
                {
                    string packagePath = AssetDatabase.GetAssetPath(package);

                    if (packagePath.EndsWith(".unitypackage"))
                    {
                        AssetDatabase.ImportPackage(packagePath, false); // false để bỏ qua xác nhận
                        Debug.Log($"Imported package: {packagePath}");
                    }
                    else
                    {
                        Debug.LogWarning($"Skipped non-unitypackage file: {packagePath}");
                    }
                }
            }
        }
    }
}