using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager i { get; private set; }

    public LevelController[] Levels;
    public LevelController CurrentLevel { get; private set; }
    private int LevelIndex;
    
    void Awake()
    {
        i = this;
        LevelIndex = 0;
        CurrentLevel = Levels[0];
    }

    public void NextLevel(){
        LevelIndex += 1;
        if(LevelIndex < Levels.Length){
            CurrentLevel = Levels[LevelIndex];
        } 
        else{
            Debug.Log("Out of Level");
        }
    }
}