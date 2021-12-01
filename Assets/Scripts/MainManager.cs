using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText; //the text that shows teh currently best score and name
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    //name of the current player
    //this only exists in the current session so use data manager instance
    string currentPlayerName;
    //name and high score value
    //this will change over the session AND between sessions
    string highScoreName;
    int highScore;

    
    // Start is called before the first frame update
    void Start()
    {
        //get the player name from the data manager - use if need to write new high score
        currentPlayerName = DataManager.instance.playerName;
        //also reset the high score each time game is restarted
        UpdateHighScoreText();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void UpdateHighScoreText(){
        //first, get the persistsent data from DataMAnager.instance
        highScoreName = DataManager.instance.GetHighScoreName();
        highScore = DataManager.instance.GetHighScore();
        //update text in UI
        string highScoreText = highScoreName+" : " + highScore;
        BestScoreText.text = highScoreText;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                 
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        //compare scores
       CheckForHighScore();
       UpdateHighScoreText();
        GameOverText.SetActive(true);
    }

    //check for high score and save temporaily using data manager instance
    //each time scene is reloaded, it will query datamanager instance
    void CheckForHighScore(){
        if(m_Points > highScore){
            DataManager.instance.SetHighScoreName(currentPlayerName);
            DataManager.instance.SetHighScore(m_Points);
        }
    }
}
