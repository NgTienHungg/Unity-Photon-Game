using UnityEngine;
using UnityEngine.UI;

namespace BaseSource
{
    public abstract class UIButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button button;

        protected virtual void Reset()
        {
            button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}