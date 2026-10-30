using UnityEngine;

public class Spider : Enemy
{
    protected enum LungeState { WindingUp, Lunging, Recovering }
    [SerializeField] private Animator _animator;

    public bool attackActive = false;
    private float lungeSpeed = 18f;
    private float windSpeed = 0.1f;
    private LungeState attackState;
    private float phaseTimer;
    private Vector2 dir;

    public bool isFacingRight = false;
    private Rigidbody2D rb;

    void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        attackCooldown = 3f;
    }

    protected override void Chasing()
    {
        movement.Tick(player);
        FlipController();
        _animator.SetBool("isRunning", true);
        _animator.SetBool("isWinding", false);
        _animator.SetBool("isLunging", false);
        if (CanSee(player) && attackTimer <= 0f)
        {
            state = EnemyState.Attack;
            attackTimer = attackCooldown; 
        }
        attackTimer -= Time.fixedDeltaTime;
    }
    
    protected override void Attack()
    {
        if (!attackActive){
            attackActive = true;
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isWinding", true);
            
            FlipController();
            
            Vector2 target = player.position;
            dir = (target - rb.position).normalized;
            phaseTimer = 0.8f;
            attackState = LungeState.WindingUp;
        }
        phaseTimer -= Time.fixedDeltaTime;
        switch (attackState)
        {
            case LungeState.WindingUp:
                rb.linearVelocity = -dir * windSpeed;
                if (phaseTimer <= 0f){
                    dir = ((Vector2)player.position - rb.position).normalized;
                    if(!CanSee(player)){
                        attackActive = false;
                        state = EnemyState.Chasing;
                    }
                    _animator.SetBool("isWinding", false);
                    _animator.SetBool("isLunging", true);
                    attackState = LungeState.Lunging;
                    phaseTimer = 0.4f;
                }
                break;

            case LungeState.Lunging:
                rb.linearVelocity = dir * lungeSpeed;
                if (phaseTimer <= 0f)
                {
                    _animator.SetBool("isLunging", false);
                    _animator.SetBool("isWinding", true);
                    attackState = LungeState.Recovering;
                    phaseTimer = 0.5f;
                }
                break;

            case LungeState.Recovering:
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 0.1f);
                if (phaseTimer <= 0f){
                    attackActive = false;
                    state = EnemyState.Chasing;
                }
                break;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }
}
