using UnityEngine.XR.ARFoundation;
using ARPortal.Runtime.Tools;
using UnityEngine;

namespace ARPortal.Runtime.EntryPoints
{
	public class ARPortalEntryPoint : MonoBehaviour
	{
		[SerializeField] private RoomPlacer _placementController;
		[SerializeField] private ARRaycastManager _raycastManager;
		[SerializeField] private ARMarker _marker;

		private void Start()
		{
			Initialize();
		}

		private void OnEnable()
		{
			SubscribeToEvents();
		}

		private void OnDisable()
		{
			UnsubscribeToEvents();
		}

		private void Initialize()
		{
			_marker.Initialize(_raycastManager);
		}

		private void SubscribeToEvents()
		{
			_marker.OnTap += _placementController.PlaceRoom;
		}

		private void UnsubscribeToEvents()
		{
			_marker.OnTap -= _placementController.PlaceRoom;
		}
	}
}
