using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    public InputField nameInput;
    
    public void StartGame() {
        //store player name
        string playerName = nameInput.text;
        DataManager.instance.playerName = playerName;

        //start game
        SceneManager.LoadScene("main");  
    }   

    public void ExitGame()
    {

         #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();

        #else
            Application.Quit();
        #endif
    }

    public void ViewHighScores(){
        SceneManager.LoadScene("highscores");
    }
}
