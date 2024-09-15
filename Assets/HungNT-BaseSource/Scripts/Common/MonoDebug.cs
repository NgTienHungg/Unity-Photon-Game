using UnityEngine;

namespace BaseSource
{
    public class MonoDebug : MonoBehaviour
    {
        protected void OnEnable()
        {
#if !DEBUG
            gameObject.SetActive(false);
#endif
        }
    }
}