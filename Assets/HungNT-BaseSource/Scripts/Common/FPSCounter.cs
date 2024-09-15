using UnityEngine;

namespace BaseSource
{
    public class FPSCounter : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT
        // for ui.
        private int screenLongSide;
        private Rect boxRect;
        private GUIStyle style = new GUIStyle();

        // for fps calculation.
        private int frameCount;
        private float elapsedTime;
        private double frameRate;
        private bool isRunning;

        private string fpsFormat = "{0} fps";

        /// <summary>
        /// Initialization
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            UpdateUISize();
        }

        private void Start()
        {
            isRunning = true;
        }

        /// <summary>
        /// Monitor changes in resolution and calculate FPS
        /// </summary>
        private void Update()
        {
            if (!isRunning) return;

            // FPS calculation
            frameCount++;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 0.5f)
            {
                frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
                frameCount = 0;
                elapsedTime = 0;

                // Update the UI size if the resolution has changed
                if (screenLongSide != Mathf.Max(Screen.width, Screen.height))
                {
                    UpdateUISize();
                }
            }
        }

        /// <summary>
        /// Resize the UI according to the screen resolution
        /// </summary>
        private void UpdateUISize()
        {
            screenLongSide = Mathf.Max(Screen.width, Screen.height);
            var rectLongSide = screenLongSide / 12f;
            boxRect = new Rect((Screen.width - rectLongSide) / 2f, 5f, rectLongSide, rectLongSide / 3f);
            style.fontSize = screenLongSide / 50;
            style.normal.textColor = Color.green;
            style.alignment = TextAnchor.MiddleCenter;
        }

        /// <summary>
        /// Display FPS
        /// </summary>
        private void OnGUI()
        {
            if (!isRunning) return;
            GUI.Box(boxRect, "");
            GUI.Label(boxRect, string.Format(fpsFormat, Mathf.Round((float)frameRate)), style);
        }
#endif
    }
}