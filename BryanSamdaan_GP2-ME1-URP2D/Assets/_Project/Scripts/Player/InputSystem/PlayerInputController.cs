using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float playerSmoothing = 0.1f;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    public int playerNumber;
    public GameObject joinPrompt;
    public bool hasJoined = false;  

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        if (GameManager.Instance.IsPlayerJoined(playerNumber))
        {
            hasJoined = true;
            joinPrompt.SetActive(false);
        }
        else
        {
            joinPrompt.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (hasJoined)
        {
            SetPlayerVelocity();
            RotatePlayer();
        }
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            playerSmoothing);

        _rigidbody.velocity = _smoothedMovementInput * moveSpeed;
    }

    private void RotatePlayer()
    {
        Vector2 direction = Vector2.zero;

        if (Gamepad.current != null && _lookInput != Vector2.zero)
        {
            direction = _lookInput;
        }
        else
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
        }

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }

    public void OnMove(InputValue inputValue)
    {
        if (hasJoined)
        {
            _movementInput = inputValue.Get<Vector2>();
        }
    }

    private void OnLook(InputValue inputValue)
    {
        if (hasJoined)
        {
            _lookInput = inputValue.Get<Vector2>();
        }
    }

    private void Update()
    {
        if (!hasJoined)
        {
            CheckJoinInput();
        }
    }

    private void CheckJoinInput()
    {
        if (playerNumber == 1 && (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame))
        {
            JoinGame();
        }
        else if (playerNumber == 2 && (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame))
        {
            JoinGame();
        }
    }

    private void JoinGame()
    {
        hasJoined = true;
        joinPrompt.SetActive(false);
        GameManager.Instance.PlayerJoined(playerNumber);
    }

    private void OnDestroy()
    {
        if (hasJoined)
        {
            GameManager.Instance.PlayerDisconnected(playerNumber);
        }
    }
}
