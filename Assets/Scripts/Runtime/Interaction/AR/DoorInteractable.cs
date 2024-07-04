using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public class DoorInteractable : BaseInteractableObject
	{
		[SerializeField] private float _sensitivity;
		[SerializeField] private float _minZOffset;
		[SerializeField] private float _maxZOffset;

		private Vector3 _firstTouchPosition;
		private Vector3 _currentTouchPosition;
		private float _finalTouchZ;
		private bool _isDragging;

		private void Update()
		{
			Dragging();
		}

		protected override void SubscribeToEvents()
		{
			base.SubscribeToEvents();
			_selectionInteractable.selectEntered.AddListener(OnSelectEntered);
			_selectionInteractable.selectExited.AddListener(OnSelectExited);
		}

		protected override void UnsubscribeToEvents()
		{
			base.UnsubscribeToEvents();
			_selectionInteractable.selectEntered.RemoveListener(OnSelectEntered);
			_selectionInteractable.selectExited.RemoveListener(OnSelectExited);
		}

		private void OnSelectEntered(SelectEnterEventArgs args)
		{
			_isDragging = true;
		}

		private void OnSelectExited(SelectExitEventArgs args)
		{
			_isDragging = false;
		}

		private void Dragging()
		{
			if (_isDragging && Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Began)
				{
					_firstTouchPosition = touch.position;
				}

				if (touch.phase == TouchPhase.Moved)
				{
					_currentTouchPosition = Input.mousePosition;

					Vector2 touchDelta = (_currentTouchPosition - _firstTouchPosition);

					_finalTouchZ = _interactableObject.transform.localPosition.z + (touchDelta.x * _sensitivity);
					_finalTouchZ = Mathf.Clamp(_finalTouchZ, _minZOffset, _maxZOffset);

					_interactableObject.transform.localPosition = new Vector3(_interactableObject.transform.localPosition.x, _interactableObject.transform.localPosition.y, _finalTouchZ);

					_firstTouchPosition = Input.mousePosition;
				}
			}
		}
	}
}
