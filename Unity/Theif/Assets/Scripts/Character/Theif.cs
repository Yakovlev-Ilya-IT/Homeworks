using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Theif : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private TheifView _view;

    private PlayerInput _input;
    private CharacterController _characterController;

    private Vector2 _inputDirection;

    private bool IsInputDiretionZero => _inputDirection == Vector2.zero;

    private void Awake()
    {
        _input = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _view.Initialize();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Character.Move.started += ctx => OnMoveStarted();
        _input.Character.Move.canceled += ctx => OnMoveCanceled();
    }

    private void OnMoveCanceled() => _view.StopMove();

    private void OnMoveStarted() => _view.StartMove();

    private void OnDisable()
    {
        _input.Disable();
        _input.Character.Move.started -= ctx => OnMoveStarted();
        _input.Character.Move.canceled -= ctx => OnMoveCanceled();
    }

    private void Update()
    {
        ReadInput();

        if (IsInputDiretionZero)
            return;

        Vector3 convertedInputDirection = GetConvertedInputDirection();
        float inputRotationAngle = GetAngleFrom(convertedInputDirection);

        Move(convertedInputDirection);
        Rotate(inputRotationAngle);
    }

    private void Move(Vector3 inputDirection)
    {
        float scaledMoveSpeed = GetScaledMoveSpeed();

        Vector3 normalizedInputDirection = inputDirection.normalized;

        _characterController.Move(normalizedInputDirection * scaledMoveSpeed);
    }

    private void Rotate(float inputAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, inputAngle, 0);
        gameObject.transform.rotation = targetRotation;
    }

    private float GetAngleFrom(Vector3 inputDirection)
    {
        float directionAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
            directionAngle += 2 * Mathf.PI * Mathf.Rad2Deg;

        return directionAngle;
    }

    private Vector3 GetConvertedInputDirection() => new Vector3(_inputDirection.x, 0, _inputDirection.y);

    private float GetScaledMoveSpeed() => _moveSpeed * Time.deltaTime;

    private void ReadInput() => _inputDirection = _input.Character.Move.ReadValue<Vector2>();
}
