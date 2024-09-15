using System.IO;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseSource
{
    public class SceneEditorWindow : OdinEditorWindow
    {
        [MenuItem("BaseSource/Scene/Scene Editor Window")]
        private static void OpenWindow()
        {
            var window = GetWindow<SceneEditorWindow>();
            window.titleContent = new GUIContent("Scene Editor");
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var sceneName = Path.GetFileNameWithoutExtension(scenePath);

                if (SirenixEditorGUI.MenuButton(i, sceneName, true, null))
                {
                    OnChangeScene(scenePath);
                }
            }
        }

        private void OnChangeScene(string scenePath)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}