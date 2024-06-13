using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.Interaction.Toolkit;
using ARPortal.Runtime.Analytics;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public abstract class BaseInteractableObject : MonoBehaviour
	{
		[SerializeField] protected GameObject _interactableObject;

		protected ARSelectionInteractable _selectionInteractable;

		private void Awake()
		{
			Initialize();
		}

		protected void OnEnable()
		{
			SubscribeToEvents();
			Debug.LogWarning(_selectionInteractable.name);
		}

		protected void OnDisable()
		{
			UnsubscribeToEvents();
		}

		protected virtual void Initialize()
		{
			_selectionInteractable = _interactableObject.GetComponent<ARSelectionInteractable>();
		}

		protected virtual void SubscribeToEvents()
		{
			_selectionInteractable.selectEntered.AddListener(OnFirebaseInteractionEvent);
		}

		protected virtual void UnsubscribeToEvents()
		{
			_selectionInteractable.selectEntered.RemoveListener(OnFirebaseInteractionEvent);
		}

		private void OnFirebaseInteractionEvent(SelectEnterEventArgs args)
		{
			FirebaseEventManager.Instance.LogInteractionEvent();
		}
	}
}
