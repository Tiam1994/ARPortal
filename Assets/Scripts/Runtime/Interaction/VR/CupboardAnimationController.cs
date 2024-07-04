using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class CupboardAnimationController : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable _interactable;
    [SerializeField] private Animator _animator;

    private bool _isDoorClosed = true;

    private readonly int _doorOpenParameterHash = Animator.StringToHash("DoorOpening");
    private readonly int _doorCloseParameterHash = Animator.StringToHash("DoorClosing");

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void UnsubscribeToEvents()
    {
        _interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (_isDoorClosed)
        {
            _animator.SetBool(_doorOpenParameterHash, true);
            _animator.SetBool(_doorCloseParameterHash, false);
            _isDoorClosed = false;
        }
        else
        {
            _animator.SetBool(_doorOpenParameterHash, false);
            _animator.SetBool(_doorCloseParameterHash, true);
            _isDoorClosed = true;
        }
    }
}
