using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImage; // reference pointing to Sprite used in _liveSprites 

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    private int _currentPlayerLives;

    void Start()
    {

        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if( _gameManager == null )
        {
            Debug.LogError("GameManager is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R) &&  _restartActive)
        //{
          //  Debug.Log("pressed 'r' with _restartActive");
            //_restartActive = false;
        //    SceneManager.LoadScene("Game");
        //}

    }
    public void UpdateScore( int playerScore )
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        // _livesImage is the reference, but .sprite is the 2d image file (the sprite)
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {

        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();

    }

    IEnumerator GameOverFlickerRoutine()
    {

        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
