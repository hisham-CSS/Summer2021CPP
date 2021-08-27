using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int maxLives = 3;

    int _score = 0;
    public int score
    {
        get { return _score; }
        set 
        {
            _score = value;
            Debug.Log("Score Changed. New Score is: " + _score);
        }
    }

    int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value && value >= 0)
            {
                //respawn code would go here
            }
            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives < 0)
            {
                //game over code will go here
            }

            Debug.Log("Lives Changed. New Lives value is: " + _lives);
        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
                SceneManager.LoadScene("TitleScreen");
            else if (SceneManager.GetActiveScene().name == "TitleScreen")
                SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            QuitGame();
        }
    }

    //PLATFORM SPECIFIC CODE EXAMPLE
    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
    }
}
