using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

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

    [Header("Weapons")]
    [SerializeField] private LayerMask _aimLayer = 0;
    [SerializeField] private Transform _weaponRoot = null;
    [SerializeField] private Weapon _weapon = null;
    private Camera _camera = null;
    private bool _useGamepad = false;
    private Vector2 _aimDir = Vector2.zero;

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _camera = Camera.main;
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
        _playerInput.Player.AimDirection.performed += OnDirection;
        _playerInput.Player.AimDirection.Enable();

        InputUser.onChange += OnInputChanged;
    }

    private void DisableInput()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.Disable();
        _playerInput.Player.Attack.performed -= OnAttack;
        _playerInput.Player.Attack.Disable();
        _playerInput.Player.AimDirection.performed -= OnDirection;
        _playerInput.Player.AimDirection.Disable();

        InputUser.onChange -= OnInputChanged;
    }

    private void OnInputChanged(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
            _useGamepad = user.controlScheme.Value.name == "Gamepad";
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _movementDir = context.ReadValue<Vector2>();
    }

    private void OnDirection(InputAction.CallbackContext context)
    {
        _aimDir = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            Attack();
    }
    #endregion

    private void Update()
    {
        UpdateMovement();
        UpdateAim();
    }

    private void UpdateMovement()
    {
        if (_rigidbody == null) return;

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

    private void UpdateAim()
    {
        if (_weaponRoot == null) return;

        float angle = 0f;
        if (_useGamepad)
        {
            angle = Mathf.Atan2(_aimDir.y, _aimDir.x);
        }
        else
        {
            Ray mouseRay = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.GetRayIntersection(mouseRay, Mathf.Infinity, _aimLayer);
            Vector2 aimDir = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
            angle = Mathf.Atan2(aimDir.y, aimDir.x);
        }

        _weaponRoot.eulerAngles = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
    }

    private void Attack()
    {
        if (_weapon == null) return;

        _weapon.DoAttack();
    }
}
