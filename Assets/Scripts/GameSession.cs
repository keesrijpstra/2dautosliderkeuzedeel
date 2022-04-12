using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    
    [SerializeField] public int playerLives = 3;
    [SerializeField] public int score = 0;
    [SerializeField] public float currentTime = 0f;
    [SerializeField] float startingTime = 10f;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    
    
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)  
        {
            Destroy(gameObject);
        }  
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
        
    }

    void Start() {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        // currentTime = startingTime;
    }

    void Update()
    {
        Timer();
        if (Input.GetKeyDown("delete"))
        {
            ProcessPlayerDeath();
        }
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }           
    }

    

    public void resetStat()
    {
        score = 0;
        scoreText.text = score.ToString();
        playerLives = 3;
        livesText.text = playerLives.ToString();
    }

    public void Timer()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");

        if (currentTime <= 0 )
        {
            currentTime = 40;
            if(playerLives > 1)
            {
                TakeLife();
            }
            else
            {
                ResetGameSession();
            }
            

        }

    }

    public void ProcessPlayerDeath()
    {

        if(playerLives > 1 )
        {
            TakeLife();
        }

        else
        {
            
            ResetGameSession();
            
        }
      
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        currentTime = 40;
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }


    public void ResetGameSession()
    {
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene("StartMenu");
        Destroy(gameObject);
        
    }
}
