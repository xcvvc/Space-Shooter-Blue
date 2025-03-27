using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    // [SerializeField]
    // private int _scoreNow;
    // private int _scoreBefore;

    // Start is called before the first frame update
    void Start()
    {
        //        _scoreNow = 0;
        //        _scoreBefore = 0;
        //        // assign text comonent to the handle
        //        _scoreText.text = "Score " + _scoreNow; // or variable from Player

        _scoreText.text = "Score: " + 0;

    }

    // Update is called once per frame
    void Update()
    {
//       if ( _scoreNow != _scoreBefore  )
//        {
//            _scoreBefore = _scoreNow;
//            _scoreText.text = "Score " + _scoreNow;
//        }

    }
    public void UpdateScore( int playerScore )
    {
        //        _scoreNow = _score;
        _scoreText.text = "Score: " + playerScore.ToString();
    }
}
