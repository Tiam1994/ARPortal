using ARPortal.Constants;
using UnityEngine;
using System;

namespace ARPortal.Runtime.PlayerLogic
{
	public class ARPlayer : MonoBehaviour
	{
		private bool _enableInteractableDetection;
		private GameObject _currentHitObject;
		private Vector2 _screenCenter;

		public event Action OnLeftRoom;
		public event Action<string> OnInteractableObjectHit;
		public event Action OnInteractableObjectLost;

		private void Update()
		{
			if(_enableInteractableDetection)
			{
				InteractableObjectDetection();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag(TagConstants.ROOM_COLLIDER))
			{
				OnLeftRoom?.Invoke();
			}
		}

		public void Initialize(bool enableInteractableDetection)
		{
			_enableInteractableDetection = enableInteractableDetection;

			if (_enableInteractableDetection)
			{
				_screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
			}
		}

		private void InteractableObjectDetection()
		{
			Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
			RaycastHit hit;
			bool hitInteractable = Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("InteractableObject"));

			if (hitInteractable)
			{
				HandleHit(hit);
			}
			else
			{
				HandleMiss();
			}
		}

		private void HandleHit(RaycastHit hit)
		{
			GameObject hitObject = hit.collider.gameObject;

			if (_currentHitObject != hitObject)
			{
				if (_currentHitObject != null)
				{
					OnInteractableObjectLost?.Invoke();
				}

				_currentHitObject = hitObject;
				OnInteractableObjectHit?.Invoke(hitObject.tag);
			}
		}

		private void HandleMiss()
		{
			if (_currentHitObject != null)
			{
				OnInteractableObjectLost?.Invoke();
				_currentHitObject = null;
			}
		}
	}
}
