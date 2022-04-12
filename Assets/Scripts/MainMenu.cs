using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI HStext;
    
    
    private void Start() 
    {
        HStext.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
        
    }
    
    // Start is called before the first frame update
    public void ExitButton()
    {
        
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    
}
