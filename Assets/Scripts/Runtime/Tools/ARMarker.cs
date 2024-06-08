using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ARPortal.Runtime.Tools
{
	public class ARMarker : MonoBehaviour
	{
		[SerializeField] private GameObject _marker;

		private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

		private ARRaycastManager _raycastManager;
		private Vector2 _screenCenter;
		private bool _isMarkerActivated;

		public event Action<Vector3> OnTap;

		private void Update()
		{
			if (_isMarkerActivated)
			{
				CreateMarker();
				HandleTap();
			}
		}

		public void Initialize(ARRaycastManager raycastManager)
		{
			_screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
			_raycastManager = raycastManager;
			_isMarkerActivated = true;
			_marker.SetActive(true);
		}

		public void Restart()
		{
			_isMarkerActivated = true;
			_marker.SetActive(true);
		}

		private void CreateMarker()
		{
			if (!_raycastManager.Raycast(_screenCenter, _hits, TrackableType.PlaneWithinPolygon))
			{
				SetMarkerActive(false);
				return;
			}

			if (_hits.Count > 0)
			{
				_marker.transform.position = _hits[0].pose.position;
			}

			SetMarkerActive(true);
		}

		private void SetMarkerActive(bool active)
		{
			if (_marker.activeSelf != active)
			{
				_marker.SetActive(active);
			}
		}

		private void HandleTap()
		{
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Vector3 tapPosition = Input.GetTouch(0).position;
				OnTap.Invoke(_marker.transform.position);
				_isMarkerActivated = false;
				_marker.SetActive(false);
			}
		}
	}
}
