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
    HighScores highScoresTemp;

    void Awake(){
        if(instance!=null){
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        highScoresTemp = LoadHighScores();
        if(highScoresTemp.playerNames==null){
            highScoresTemp.playerNames = new List<string>();
            highScoresTemp.playerNames.AddRange(new string[]{"--","--","--","--","--"});
        }
        if(highScoresTemp.playerScores==null){
            highScoresTemp.playerScores = new List<int>();
            highScoresTemp.playerScores.AddRange(new int[]{0,0,0,0,0});
        }
    }

    //getters and setters - just accesses the variables
    public int GetHighScore(){
        return highScore;
    }

    public string GetHighScoreName(){
        return highScoreName;
    }

    public List<string> GetHighScoreNames(){
        return highScoresTemp.playerNames;
    }

    public List<int> GetHighScores(){
        return highScoresTemp.playerScores;
    }

    //sets the high score variables
    public void SetHighScore(int score){
        highScore = score;
    }

    public void SetHighScoreName(string player){
        highScoreName = player;
    }

    //save data between sessions
    //Score object will be the currrent high score data
    [System.Serializable]
    public class HighScores {
        public List<string> playerNames;
        public List<int> playerScores;
    }

    //check to see if this score belongs in the list of high scores
    public void CheckScore(int pts){
        int scoreInsertIndex = 5;
        for(int i=0;i<highScoresTemp.playerScores.Count;i++){
            if(pts >= highScoresTemp.playerScores[i]){
                scoreInsertIndex = i;
                break;
            }
        }
        if(scoreInsertIndex<5){
            highScoresTemp.playerNames.Insert(scoreInsertIndex,playerName);
            highScoresTemp.playerScores.Insert(scoreInsertIndex,pts);
        }

        //make sure list is no longer than 5 items
        if(highScoresTemp.playerNames.Count>5){
            highScoresTemp.playerNames.RemoveAt(5);
        }
        if(highScoresTemp.playerScores.Count>5){
            highScoresTemp.playerScores.RemoveAt(5);
        }

        //lastly, save the updated list
        SaveHighScores();
    }

    public void SaveHighScores(){
        
        string json = JsonUtility.ToJson(highScoresTemp);

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public HighScores LoadHighScores(){
        HighScores highScores = new HighScores();
        string path = Application.persistentDataPath + "/highscores.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScores = JsonUtility.FromJson<HighScores>(json);

            highScoreName = highScores.playerNames[0];
            highScore = highScores.playerScores[0];
        }
        return highScores;
    }
}