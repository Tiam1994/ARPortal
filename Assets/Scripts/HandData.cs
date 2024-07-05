using UnityEngine;

public class HandData : MonoBehaviour
{
	[SerializeField] private HandModelType _handType;
	[SerializeField] private Animator _animator;
	[SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

	public HandModelType HandType => _handType;

	public void HandModelSetActive(bool enabled)
	{
		_animator.enabled = enabled;
		_skinnedMeshRenderer.enabled = enabled;
	}
}
