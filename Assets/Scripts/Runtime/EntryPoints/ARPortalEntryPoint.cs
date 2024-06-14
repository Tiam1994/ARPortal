using ARPortal.Runtime.ResourcesLoading;
using ARPortal.Runtime.PlayerLogic;
using UnityEngine.XR.ARFoundation;
using ARPortal.Runtime.Analytics;
using ARPortal.Runtime.Tools;
using System.Threading.Tasks;
using ARPortal.Runtime.UI;
using UnityEngine;

namespace ARPortal.Runtime.EntryPoints
{
	public class ARPortalEntryPoint : MonoBehaviour
	{
		[SerializeField] private RoomPlacer _roomPlacer;
		[SerializeField] private PostersLoader _postersLoader;
		[SerializeField] private ARRaycastManager _raycastManager;
		[SerializeField] private ARMarker _marker;
		[SerializeField] private ARPlayer _player;
		[SerializeField] private UIManager _uiManager;

		private InitialLaunchDetector _initialLaunchDetector = new InitialLaunchDetector();

		private float _sessionStartTime;

		private async void Start()
		{
			await InitializeAsync();
		}

		private async Task InitializeAsync()
		{
			await FirebaseEventManager.Instance.InitializeAsync();

			_initialLaunchDetector.InitializeUniqueIdentifier();
			_marker.Initialize(_raycastManager);
			_postersLoader.DownloadImages();
			_player.Initialize(_initialLaunchDetector.IsFirstApplicationLaunch);

			SubscribeToEvents();
			StartSession();
		}

		private void StartSession()
		{
			_sessionStartTime = Time.time;

			if(_initialLaunchDetector.IsFirstApplicationLaunch)
			{
				_uiManager.ActivateTooltips();
			}
			else
			{
				_uiManager.HideTooltips();
			}

			FirebaseEventManager.Instance.LogStartSessionEvent();
		}

		private void EndSession()
		{
			float timeSpent = Time.time - _sessionStartTime;
			FirebaseEventManager.Instance.LogEndSessionEvent(timeSpent);
		}

		private void SubscribeToEvents()
		{
			_marker.OnTap += _roomPlacer.PlaceRoom;
			_player.OnLeftRoom += RestartRoomPlacement;

			if(_initialLaunchDetector.IsFirstApplicationLaunch)
			{
				_roomPlacer.OnRoomPlaced += _uiManager.HideTooltips;
				_player.OnInteractableObjectHit += _uiManager.ShowTooltipForInteractionObjects;
				_player.OnInteractableObjectLost += _uiManager.HideTooltips;
			}
		}

		private void UnsubscribeToEvents()
		{
			_marker.OnTap -= _roomPlacer.PlaceRoom;
			_player.OnLeftRoom -= RestartRoomPlacement;

			if (_initialLaunchDetector.IsFirstApplicationLaunch)
			{
				_roomPlacer.OnRoomPlaced -= _uiManager.HideTooltips;
				_player.OnInteractableObjectHit -= _uiManager.ShowTooltipForInteractionObjects;
				_player.OnInteractableObjectLost -= _uiManager.HideTooltips;
			}
		}

		private void RestartRoomPlacement()
		{
			_marker.Restart();
			_roomPlacer.HideRoom();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				EndSession();
			}
			else
			{
				StartSession();
			}
		}

		private void OnApplicationQuit()
		{
			UnsubscribeToEvents();
		}
	}
}
