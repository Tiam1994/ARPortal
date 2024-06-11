using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public class CupboardInteractable : BaseInteractableObject
	{
		private Animator _animator;
		private bool _isDoorClosed;

		private readonly int _doorOpenParameterHash = Animator.StringToHash("DoorOpening");
		private readonly int _doorCloseParameterHash = Animator.StringToHash("DoorClosing");

		protected override void Initialize()
		{
			base.Initialize();
			_animator = _interactableObject.GetComponent<Animator>();
			_isDoorClosed = true;
		}

		protected override void SubscribeToEvents()
		{
			base.SubscribeToEvents();
			_selectionInteractable.selectEntered.AddListener(OnSelectEntered);
		}

		protected override void UnsubscribeToEvents()
		{
			base.UnsubscribeToEvents();
			_selectionInteractable.selectEntered.RemoveListener(OnSelectEntered);
		}

		private void OnSelectEntered(SelectEnterEventArgs args)
		{
			PlayAnimation();
		}

		private void PlayAnimation()
		{
			if (_isDoorClosed)
			{
				_animator.SetBool(_doorOpenParameterHash, true);
				_animator.SetBool(_doorCloseParameterHash, false);
				_isDoorClosed = false;
			}
			else
			{
				_animator.SetBool(_doorOpenParameterHash, false);
				_animator.SetBool(_doorCloseParameterHash, true);
				_isDoorClosed = true;
			}
		}
	}
}
