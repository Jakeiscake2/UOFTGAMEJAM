using UnityEngine;

public class DepositStation : MonoBehaviour
{
    public ColorClass requiredColor;
    private SpriteRenderer RequiredSpriteRender;

    private void Awake(){
        RequiredSpriteRender = transform.Find("ColorCanHolder").GetComponent<SpriteRenderer>();
        // requiredColor = ColorClass.Blank;
    }

    public void setRequiredColor(ColorClass setColor){
        requiredColor = setColor;
        RequiredSpriteRender.sprite = ColorCanLibrary.Get(setColor);
    }

    private void Update(){
        float alphaPer = Mathf.PingPong(Time.time * 2, 1.0f);

        RequiredSpriteRender.color = new Color(1, 1, 1, Mathf.Lerp(0.2f, 0.9f, alphaPer));
    }
}

/*


// Debug: flash between two colours, faster as the fuse runs down.
    private void FlashFuse()
    {
        if (sprite == null) return;

        float t = 1f - Mathf.Clamp01(_fuse / fuseTime);        // 0 when armed -> 1 at detonation
        float period = Mathf.Lerp(slowFlash, fastFlash, t);    // flash interval shrinks over time

        _flashTimer += Time.fixedDeltaTime;
        if (_flashTimer >= period)
        {
            _flashTimer = 0f;
            _flashOn = !_flashOn;
            sprite.color = _flashOn ? dangerColor : calmColor;
        }
    }
    */