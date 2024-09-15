using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaseSource
{
    public class UILoadingScreen : MonoBehaviour
    {
        // [SerializeField] private Image progressFill;
        [SerializeField] private TextMeshProUGUI progressText;
        // [SerializeField] private TextMeshProUGUI loadingText;

        private float _currentPercent;

        public void SetProgress(float progress)
        {
            DOVirtual.Float(
                _currentPercent, progress.Percent(), 0.3f,
                x =>
                {
                    _currentPercent = x;
                    if (progressText) progressText.text = x.Int() + "%";
                    // if (progressFill) progressFill.fillAmount = x / 100f;
                });
        }

        public void Show()
        {
            gameObject.SetActive(true);
            progressText.text = "0%";
            _currentPercent = 0;
            // StartCoroutine(IEUpdateLoadingText());
        }

        // private IEnumerator IEUpdateLoadingText()
        // {
        //     var waitUpdateDots = new WaitForSeconds(0.5f);
        //     while (true)
        //     {
        //         loadingText.text = "Loading";
        //         yield return waitUpdateDots;
        //         loadingText.text = "Loading.";
        //         yield return waitUpdateDots;
        //         loadingText.text = "Loading..";
        //         yield return waitUpdateDots;
        //         loadingText.text = "Loading...";
        //         yield return waitUpdateDots;
        //     }
        // }

        public void Hide()
        {
            GetComponent<Animator>().SetTrigger("Close");
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}