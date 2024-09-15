using UnityEngine;

namespace BaseSource
{
    public class SRDebugInit : MonoBehaviour
    {
#if DEBUG
        private void Start()
        {
            SRDebug.Init();
        }
#endif
    }
}