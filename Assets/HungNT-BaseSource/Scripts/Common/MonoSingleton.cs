using UnityEngine;

namespace BaseSource
{
    public abstract class MonoSingleton<TMono> : MonoBehaviour where TMono : MonoBehaviour
    {
        private static TMono _instance;

        public static TMono Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TMono>();
                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(TMono).Name);
                        _instance = go.AddComponent<TMono>();
                        Debug.Log("[MonoSingleton] Create new instance of " + typeof(TMono).Name.Color("yellow"), _instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as TMono;
                OnAwake();
                return;
            }

            Destroy(gameObject);
        }

        protected abstract void OnAwake();
    }
}