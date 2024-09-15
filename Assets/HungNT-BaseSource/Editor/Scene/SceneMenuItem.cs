using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace BaseSource
{
    public class SceneMenuItem
    {
        [MenuItem("BaseSource/Scene/Save And Play Game")]
        public static void QuickSaveAndPlay()
        {
            EditorSceneManager.SaveOpenScenes(); // Lưu scene hiện tại mà không hỏi
            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
            EditorApplication.isPlaying = true;
        }
    }
}