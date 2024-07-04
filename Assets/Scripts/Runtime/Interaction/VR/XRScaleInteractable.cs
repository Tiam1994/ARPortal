using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Linq;

namespace ARPortal.Runtime.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class XRScaleInteractable : XRBaseInteractable
    {
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _heightScaleFactor;

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
                    ScaleObject();
                }
            }
        }

        private void ScaleObject()
        {
            IXRSelectInteractor interactor = interactorsSelecting.First();

            float currentControllerHeight = interactor.transform.position.y;

            float heightDifference = currentControllerHeight - _initialControllerHeight;

            float newScaleFactor = 1.0f + (heightDifference * _heightScaleFactor);
            newScaleFactor = Mathf.Clamp(newScaleFactor, _minScale, _maxScale);

            Vector3 newScale = _initialScale * newScaleFactor;
            transform.localScale = newScale;

            _currentScaleFactor = newScaleFactor;
        }
    }
}
