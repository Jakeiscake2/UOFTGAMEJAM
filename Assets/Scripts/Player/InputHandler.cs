using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{

    [SerializeField] PlayerInput _playerInput;
    public static InputHandler i { get; set; }
    private Vector2 move;
    private bool debug;
    private bool pausePressed;
    private bool attack;
    private bool interact;

    private InputAction _moveAction;
    private InputAction _debugAction;
    private InputAction _pauseAction;
    private InputAction _interactAction;

    private void Awake()
    {
        i = this;
        _moveAction = _playerInput.actions["Move"];
        _debugAction = _playerInput.actions["Debug"];
        _pauseAction = _playerInput.actions["Pause"];
        _interactAction = _playerInput.actions["Interact"];
    }
    private void Update()
    {
        pausePressed = _pauseAction.WasPressedThisFrame();
        // if(PauseMenu.isPaused) return;
        // Input disabled during pause
        move = _moveAction.ReadValue<Vector2>();
        debug = _debugAction.WasPressedThisFrame();
        interact = _interactAction.WasPressedThisFrame();
    }
    public Vector2 Move => move;
    public bool Inspect => debug;
    public bool Pause => pausePressed;
    public bool Interact => interact;
}
