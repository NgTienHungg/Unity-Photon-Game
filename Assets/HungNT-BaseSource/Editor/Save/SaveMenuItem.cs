using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BaseSource
{
    public static class SaveMenuItem
    {
        [MenuItem("BaseSource/Save/Clear Save Data")]
        private static void ClearData()
        {
            PlayerPrefs.DeleteAll(); // Xóa PlayerPref
            Debug.Log("Data cleared!".Color("lime"));
        }

        [MenuItem("BaseSource/Save/Open Save Folder")]
        public static void OpenSaveFolder()
        {
            var saveFolderPath = Application.persistentDataPath;

            if (!string.IsNullOrEmpty(saveFolderPath))
            {
                Process.Start("explorer.exe", saveFolderPath.Replace("/", "\\"));
            }
            else
            {
                Debug.LogError("Save data folder path is invalid or could not be found.");
            }
        }
    }
}