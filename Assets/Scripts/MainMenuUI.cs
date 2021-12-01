using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public InputField nameInput;
    
    public void StartGame() {
        //store player name
        string playerName = nameInput.text;
        Debug.Log(playerName);

        //start game
        SceneManager.LoadScene("main");  
    }   
}
