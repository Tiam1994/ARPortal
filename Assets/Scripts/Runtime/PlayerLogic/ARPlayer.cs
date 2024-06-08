using ARPortal.Constants;
using UnityEngine;
using System;

namespace ARPortal.Runtime.PlayerLogic
{
	public class ARPlayer : MonoBehaviour
	{
		public event Action OnLeftRoom;

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag(TagConstants.ROOM_COLLIDER))
			{
				OnLeftRoom?.Invoke();
			}
		}
	}
}
