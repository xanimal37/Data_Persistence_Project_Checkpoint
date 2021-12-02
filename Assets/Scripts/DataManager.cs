using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    //getters and setters - just helps to organize 
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

    //save data between sessions
    //Score object will be the currrent high score data
    [System.Serializable]
    class Score {
        public string playerName;
        public int points;
    }

    public void SaveHighScore(){
        Score highscore = new Score();
        highscore.playerName=highScoreName;
        highscore.points=highScore;

        string json = JsonUtility.ToJson(highscore);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore(){
        string path = Application.persistentDataPath + "/highscore.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Score highscore = JsonUtility.FromJson<Score>(json);

            highScoreName = highscore.playerName;
            highScore = highscore.points;
        }
    }
}
