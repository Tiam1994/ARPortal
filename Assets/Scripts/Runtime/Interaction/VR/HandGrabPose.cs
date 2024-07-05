using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public class HandGrabPose : MonoBehaviour
	{
		[SerializeField] private XRGrabInteractable _interactable;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private SkinnedMeshRenderer _leftSkinnedMeshRenderer;
		[SerializeField] private SkinnedMeshRenderer _rightSkinnedMeshRenderer;

		private void OnEnable()
		{
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			_interactable.selectEntered.AddListener(ActivateGrabPose);
			_interactable.selectExited.AddListener(DeactivateGrabPose);
		}

		private void ActivateGrabPose(SelectEnterEventArgs args)
		{
			if (args.interactorObject is XRDirectInteractor)
			{
				HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
				handData.HandModelSetActive(false);

				switch (handData.HandType)
				{
					case HandModelType.Right:
						_rightSkinnedMeshRenderer.enabled = true;
						break;
					case HandModelType.Left:
						_leftSkinnedMeshRenderer.enabled = true;
						break;
				}
			}
		}

		private void DeactivateGrabPose(SelectExitEventArgs args)
		{
			if (args.interactorObject is XRDirectInteractor)
			{
				HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
				handData.HandModelSetActive(true);

				switch (handData.HandType)
				{
					case HandModelType.Right:
						_rightSkinnedMeshRenderer.enabled = false;
						break;
					case HandModelType.Left:
						_leftSkinnedMeshRenderer.enabled = false;
						break;
				}

				_rigidbody.isKinematic = false;
			}
		}
	}
}
