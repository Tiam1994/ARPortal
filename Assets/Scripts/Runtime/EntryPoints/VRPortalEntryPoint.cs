using ARPortal.Runtime.ResourcesLoading;
using UnityEngine;

namespace ARPortal.Runtime.EntryPoints
{
    public class VRPortalEntryPoint : MonoBehaviour
    {
        [SerializeField] private PostersLoader _postersLoader;

        private void Start()
        {
            _postersLoader.DownloadImages();
        }
    }
}
