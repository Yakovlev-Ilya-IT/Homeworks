using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TheifView : MonoBehaviour
{
    private const string IsRunning = "IsRunning";

    private Animator _animator;

    private int _isRunningHash;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _isRunningHash = Animator.StringToHash(IsRunning);
    }

    public void StartMove() => _animator.SetBool(_isRunningHash, true);

    public void StopMove() => _animator.SetBool(_isRunningHash, false);
}
