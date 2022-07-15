using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInput = null;
    private Rigidbody2D _rigidbody = null;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _verticalMultiplier = 0.8f;
    private Vector2 _movementDir = Vector2.zero;

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();

    }

    #region INPUT
    private void EnableInput()
    {
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.Enable();
        _playerInput.Player.Attack.performed += OnAttack;
        _playerInput.Player.Attack.Enable();
    }

    private void DisableInput()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.Disable();
        _playerInput.Player.Attack.performed -= OnAttack;
        _playerInput.Player.Attack.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _movementDir = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            Attack();
    }
    #endregion

    private void Update()
    {
        if (_movementDir.sqrMagnitude > 0.1f)
        {
            Vector2 movement = _movementDir * _movementSpeed;
            movement.y *= _verticalMultiplier;

            _rigidbody.velocity = movement;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
