using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace ARPortal.Runtime.Interaction
{
	public class HandData : MonoBehaviour
	{
		[SerializeField] private HandModelType _handType;
		[SerializeField] private Animator _animator;
		[SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[SerializeField] private XRInteractorLineVisual _lineVisual;

		public HandModelType HandType => _handType;

		public void HandModelSetActive(bool enabled)
		{
			_animator.enabled = enabled;
			_skinnedMeshRenderer.enabled = enabled;
			_lineVisual.enabled = enabled;
		}
	}
}
