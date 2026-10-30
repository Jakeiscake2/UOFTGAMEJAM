using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    // Base Stats 
    [SerializeField] AnimationCurve SpeedCurve;
    private PlayerItemManager playerItemManager;
    private InputHandler _input;
    public float moveX;
    public float moveY;
    private Rigidbody2D rb;
    private StationScript InteractableStation;
    private bool InteractableTrash;
    private DepositStation InteractableDepositStation;
    
    //animation
    [SerializeField] private Animator _animator;
    public bool isFacingRight = false;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerItemManager = transform.GetComponent<PlayerItemManager>();
        InteractableTrash = false;
    }

    void Start()
    {
        _input = InputHandler.i;
    }

    void Update()
    {
        Move();
        FlipController();
        if(_input.Interact){     
            if(InteractableStation != null){
                playerItemManager.giveColor(InteractableStation.storedColor);
            }
            if(InteractableTrash){
                playerItemManager.removeColor();
            }
            if(InteractableDepositStation != null){
                if(InteractableDepositStation.requiredColor == playerItemManager.HeldColor){
                    Debug.Log("Color Taken");
                    playerItemManager.removeColor();
                    LevelManager.i.CurrentLevel.NextDeposit();
                }
                else{
                    Debug.Log("Wrong Color");
                    //Play sound or vibrate or smth to show
                }
                //Set some global conditions here
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX, moveY) * baseSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Station"))
        {
            var station = collision.GetComponent<StationScript>();
            InteractableStation = station;
        }
        if(collision.CompareTag("Trash")){
            InteractableTrash = true;
        }
        if(collision.CompareTag("Deposit")){
            var deposit = collision.GetComponent<DepositStation>();
            InteractableDepositStation = deposit;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Station"))
        {
            InteractableStation = null;
        }
        if(collision.CompareTag("Trash")){
            InteractableTrash = false;
        }
        if(collision.CompareTag("Deposit")){
            InteractableDepositStation = null;
        }
    }

    private void Move()
    {
        moveX = _input.Move.x;
        moveY = _input.Move.y;
        // animator
        if (moveX != 0 || moveY != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (moveX > 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && !isFacingRight)
        {
            Flip();
        }
    }
}
