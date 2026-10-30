using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    public ColorClass HeldColor;
    private SpriteRenderer HeldSpriteRender;

    private void Awake(){
        HeldSpriteRender = transform.Find("Hand").GetComponent<SpriteRenderer>();
        // HeldColor = ColorClass.Blank;
    }

    public void updateSprite(ColorClass color){
        HeldSpriteRender.sprite = ColorCanLibrary.Get(color);
    }
    
    public void giveColor(ColorClass color)
    {
        HeldColor = HeldColor | color;
        // Debug.Log(HeldColor);
        updateSprite(HeldColor);    
    }

    public void removeColor()
    {
        HeldColor = ColorClass.Blank;
        HeldSpriteRender.sprite = null;
    }
}
