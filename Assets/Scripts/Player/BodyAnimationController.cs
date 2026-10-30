using UnityEngine;

public class BodyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // [SerializeField] private AudioSource audioSource;
    
    private PlayerController player;
    private float footstepTimer;
    
    void Start()
    {
        player = GetComponentInParent<PlayerController>(); 
    }

    void Update()
    {
        // animator
        if (player.moveX != 0 || player.moveY != 0)
        {
            _animator.SetBool("isRunning", true);
            float speed = new Vector2(player.moveX, player.moveY).magnitude;
            footstepTimer -= Time.deltaTime * (1 + Mathf.Clamp01(speed / 10f));
            if (footstepTimer <= 0)
            {
                // AudioManager.Instance.PlayAtPosition("playerFootsteps", transform.position, 0.3f);
                footstepTimer = 0.5f;
            }
        }
        else
        {
            _animator.SetBool("isRunning", false);
            footstepTimer = 0;
        }
    }
}
