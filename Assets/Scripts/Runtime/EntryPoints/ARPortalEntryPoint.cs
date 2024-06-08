using ARPortal.Runtime.PlayerLogic;
using UnityEngine.XR.ARFoundation;
using ARPortal.Runtime.Tools;
using UnityEngine;

namespace ARPortal.Runtime.EntryPoints
{
	public class ARPortalEntryPoint : MonoBehaviour
	{
		[SerializeField] private RoomPlacer _roomPlacer;
		[SerializeField] private ARRaycastManager _raycastManager;
		[SerializeField] private ARMarker _marker;
		[SerializeField] private ARPlayer _player;

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

		private void Restart()
		{
			_marker.Restart();
			_roomPlacer.HideRoom();
		}

		private void SubscribeToEvents()
		{
			_marker.OnTap += _roomPlacer.PlaceRoom;
			_player.OnLeftRoom += Restart;
		}

		private void UnsubscribeToEvents()
		{
			_marker.OnTap -= _roomPlacer.PlaceRoom;
			_player.OnLeftRoom -= Restart;
		}
	}
}
