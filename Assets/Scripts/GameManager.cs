using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;   // starts out false

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if r key pressed, restart current scene
        if( Input.GetKeyDown(KeyCode.R) && _isGameOver )
        {
            SceneManager.LoadScene(0);  // ID 0 is scene "Game"
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
