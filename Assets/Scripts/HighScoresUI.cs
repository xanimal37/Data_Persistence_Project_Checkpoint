using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresUI : MonoBehaviour
{
    public List<Text> scoreSlots;

    void Start(){
        UpdateHighScores();
    }

    void UpdateHighScores(){
        List<string> playerNames = DataManager.instance.GetHighScoreNames();
        List<int> playerScores = DataManager.instance.GetHighScores();

        for(int i=0;i<playerNames.Count;i++){
            string scoreText = playerNames[i] + " -- " + playerScores[i];
            scoreSlots[i].text = scoreText;
        }
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
