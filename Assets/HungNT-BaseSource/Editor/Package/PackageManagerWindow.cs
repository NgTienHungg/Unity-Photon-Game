// using System.Collections.Generic;
// using System.IO;
// using Newtonsoft.Json.Linq;
// using Sirenix.OdinInspector;
// using Sirenix.OdinInspector.Editor;
// using UnityEditor;
// using UnityEngine;
//
// namespace BaseSource
// {
//     public class PackageManagerWindow : OdinEditorWindow
//     {
//         [ReadOnly]
//         public List<string> unityPackages = new List<string>();
//         public List<string> otherPackages = new List<string>();
//
//         [MenuItem("BaseSource/Package/Package Manager Window")]
//         private static void OpenWindow()
//         {
//             GetWindow<PackageManagerWindow>().Show();
//         }
//
//         protected override void OnEnable()
//         {
//             base.OnEnable();
//             LoadManifestFile();
//         }
//
//         [GUIColor("cyan")]
//         [ButtonGroup("Buttons")]
//         [Button(ButtonSizes.Large)]
//         private void LoadManifestFile()
//         {
//             string manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
//
//             if (File.Exists(manifestPath))
//             {
//                 string jsonContent = File.ReadAllText(manifestPath);
//                 JObject manifest = JObject.Parse(jsonContent);
//                 JObject dependencies = manifest["dependencies"] as JObject;
//
//                 unityPackages.Clear();
//                 otherPackages.Clear();
//
//                 foreach (var package in dependencies)
//                 {
//                     string packageName = package.Key;
//                     string packageUrlOrVersion = package.Value.ToString();
//
//                     if (packageName.StartsWith("com.unity."))
//                     {
//                         unityPackages.Add($"{packageName} : {packageUrlOrVersion}");
//                     }
//                     else
//                     {
//                         otherPackages.Add($"{packageName} : {packageUrlOrVersion}");
//                     }
//                 }
//             }
//             else
//             {
//                 Debug.LogError("manifest.json file not found.");
//             }
//         }
//
//         [GUIColor("green")]
//         [ButtonGroup("Buttons")]
//         [Button(ButtonSizes.Large)]
//         private void UpdateManifestFile()
//         {
//             var manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
//             var manifest = new JObject();
//             var dependencies = new JObject();
//
//             // Add Unity packages
//             foreach (var package in unityPackages)
//             {
//                 var parts = package.Split(new[] { " : " }, System.StringSplitOptions.None);
//                 if (parts.Length == 2)
//                 {
//                     dependencies[parts[0]] = parts[1];
//                 }
//             }
//
//             // Add other packages
//             foreach (var package in otherPackages)
//             {
//                 var parts = package.Split(new[] { " : " }, System.StringSplitOptions.None);
//                 if (parts.Length == 2)
//                 {
//                     dependencies[parts[0]] = parts[1];
//                 }
//             }
//
//             manifest["dependencies"] = dependencies;
//
//             File.WriteAllText(manifestPath, manifest.ToString());
//             AssetDatabase.Refresh();
//             Debug.Log("Manifest updated successfully!");
//         }
//     }
// }