using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public class TVInteractable : BaseInteractableObject
	{
		[SerializeField] private Transform _tvAttachPoint;

		private bool _isHeightAdjustment;

		private void Update()
		{
			HeightAdjustment();
		}

		protected override void Initialize()
		{
			base.Initialize();
			_isHeightAdjustment = false;
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
			_isHeightAdjustment = true;
		}

		private void OnSelectExited(SelectExitEventArgs args)
		{
			_isHeightAdjustment = false;
		}

		private void HeightAdjustment()
		{
			if (_isHeightAdjustment)
			{
				_interactableObject.transform.position = _tvAttachPoint.position;
			}
		}
	}
}
