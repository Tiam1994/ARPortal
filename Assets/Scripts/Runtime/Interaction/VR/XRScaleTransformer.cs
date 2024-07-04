using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Linq;

namespace ARPortal.Runtime.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class XRScaleTransformer : XRBaseInteractable
    {
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _sensitivity;

        private Vector3 _initialScale;
        private float _initialControllerHeight;
        private float _currentScaleFactor = 1.0f;

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            _initialScale = transform.localScale;
            _initialControllerHeight = args.interactorObject.transform.position.y;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    Scaleing();
                }
            }
        }

        private void Scaleing()
        {
            IXRSelectInteractor interactor = interactorsSelecting.First();

            float currentControllerHeight = interactor.transform.position.y;
            float heightDifference = currentControllerHeight - _initialControllerHeight;

            float newScaleFactor = 1.0f + (heightDifference * _sensitivity);

            Vector3 newScale = _initialScale * newScaleFactor;

            newScale.x = Mathf.Clamp(newScale.x, _minScale, _maxScale);
            newScale.y = Mathf.Clamp(newScale.y, _minScale, _maxScale);
            newScale.z = Mathf.Clamp(newScale.z, _minScale, _maxScale);

            transform.localScale = newScale;

            _currentScaleFactor = newScaleFactor;
        }
    }
}
