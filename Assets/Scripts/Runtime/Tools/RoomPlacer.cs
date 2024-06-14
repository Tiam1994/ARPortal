using UnityEngine;
using System;

namespace ARPortal.Runtime.Tools
{
	public class RoomPlacer : MonoBehaviour
	{
		[SerializeField] private Transform _room;
		[SerializeField] private float _rotationOffsetY;

		public event Action OnRoomPlaced;

		public void PlaceRoom(Vector3 targetPosition)
		{
			_room.gameObject.SetActive(true);
			SetupRoomPosition(targetPosition);
			OnRoomPlaced?.Invoke();
		}

		public void HideRoom()
		{
			_room.gameObject.SetActive(false);
		}

		private void SetupRoomPosition(Vector3 targetPosition)
		{
			_room.position = targetPosition;

			Vector3 directionToCamera = Camera.main.transform.position - _room.position;
			directionToCamera.y = 0f;

			Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
			_room.rotation = lookRotation * Quaternion.Euler(0f, _rotationOffsetY, 0f);
		}
	}
}
