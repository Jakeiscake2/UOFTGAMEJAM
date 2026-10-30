using UnityEngine;

public class RandomHands : MonoBehaviour
{
    public GameObject Player;

    private void Awake(){
        Player.GetComponent<PlayerItemManager>().giveColor(ColorClass.Red);
    }
}