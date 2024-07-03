using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace ARPortal.Runtime.Interaction
{
	[RequireComponent (typeof (Rigidbody))]
	public class XRRotateInteractable : XRBaseInteractable
	{
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _sensitivity;

        private Vector3 _previousInteractorLocalPosition;
        private float _currentAngle = 0.0f;

        public UnityEvent<float> OnRotate;

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            _previousInteractorLocalPosition = GetLocalInteractorPoint(args.interactorObject.transform.position);
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    Rotate();
                }
            }
        }

        private void Rotate()
        {
            IXRSelectInteractor interactorSelecting = interactorsSelecting.First();

            Vector3 newPosition = GetLocalInteractorPoint(interactorSelecting.transform.position);

            float result = Vector3.SignedAngle(_previousInteractorLocalPosition, newPosition, Vector3.up) * _sensitivity;

            float deltaAngle = result;
            float newAngle = _currentAngle + deltaAngle;

            if (newAngle > _maxAngle)
            {
                deltaAngle = _maxAngle - _currentAngle;
                _currentAngle = _maxAngle;
            }
            else if (newAngle < _minAngle)
            {
                deltaAngle = _minAngle - _currentAngle;
                _currentAngle = _minAngle;
            }
            else
            {
                _currentAngle = newAngle;
            }

            transform.Rotate(Vector3.up, deltaAngle, Space.World);

            _previousInteractorLocalPosition = GetLocalInteractorPoint(interactorSelecting.transform.position);

            OnRotate?.Invoke(_currentAngle);
        }

        private Vector3 GetLocalInteractorPoint(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            position = position.normalized;
            return position;
        }
    }
}
