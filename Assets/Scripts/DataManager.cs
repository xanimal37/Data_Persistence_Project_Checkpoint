using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string playerName;
    //data to save in session
    string highScoreName = "--";
    int highScore = 0;
    //data to save between sessions

    void Awake(){
        if(instance!=null){
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetHighScore(){
        return highScore;
    }

    public string GetHighScoreName(){
        return highScoreName;
    }

    public void SetHighScore(int score){
        highScore = score;
    }

    public void SetHighScoreName(string player){
        highScoreName = player;
    }


}
