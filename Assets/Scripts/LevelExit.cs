using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishSFX;
    public int score;
    
    
    
    void Start() 
    {
        int score = FindObjectOfType<GameSession>().score; 
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            
            finishEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(finishSFX);
            StartCoroutine(LoadNextLevel());
            
        }
             
        
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
             
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            FindObjectOfType<GameSession>().resetStat();
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        FindObjectOfType<GameSession>().currentTime = 40;
        

    }

    

}



