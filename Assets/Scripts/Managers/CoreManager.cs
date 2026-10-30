using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        //Put core startup for the game
        //Load audio, save system
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu)
            .Perform();
    }
}