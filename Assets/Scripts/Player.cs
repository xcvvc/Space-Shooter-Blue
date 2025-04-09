using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private float _speedBoostTimeWindow = 5.0f;

    private bool _isSpeedBoostActive;
    private bool _areShieldsActive = false;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;


    // variable reference to the Shield visualizer
    [SerializeField]
    private GameObject _shieldVisualizer;

    private float _bottomEdge = -2.5f;
    // private float _topEdge = 8.0f;
    private float _leftEdge = -8.0f;
    private float _rightEdge = 8.0f;

    float horizontalInput;
    float verticalInput;
    Vector3 position;

    private Vector3 _laserStart = new Vector3(0, 1.05f, 0);  // position above Player object
    [SerializeField]
    private float _fireRate = 0.5f;  // space between firing
    private float _canFire = -1f;   // negative to okay firing starting out

    private bool _isTripleShotActive;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UIManager _uIManager;


    [SerializeField]
    private float _canTripleFireTimeWindow = 5.0f;

    [SerializeField]
    private int _score = 0;

    // reference variable to store audio clip
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        // find gamecomponent and assign type
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager.SpawnManager is missing.");
        }

        if ( _uIManager == null )
        {
            Debug.LogError("Canvas.UI_Manager is missing.");
        }

        if ( _audioSource == null )
        {
            Debug.LogError("AudioSource is missing.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        transform.position = new Vector3(0, 0, 0);
        _shieldVisualizer.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        // press a space key to fire a prefab laser object, to create a laser which moves up

        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            FireLaser();
        }

    }
    void CalculateMovement()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        position = new Vector3(horizontalInput, verticalInput, 0);
        
        transform.Translate(position * _speed * Time.deltaTime);
       

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _bottomEdge + 0.5f, 0), 0);

        if (transform.position.x >= _rightEdge)
        {
            transform.position = new Vector3(_leftEdge, transform.position.y, 0);
        }
        else if (transform.position.x <= _leftEdge)
        {
            transform.position = new Vector3(_rightEdge, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserStart, Quaternion.identity);
        }

        // play the laser audio clip
        _audioSource.Play();
    }

    public void Damage()
    {
        // if shields is active, do nothing
        // deactivate shields
        // return
        if( _areShieldsActive )
        {
            _areShieldsActive = false;
            // disable the visualizer
            _shieldVisualizer.SetActive(false);

            return;
        }

        _lives -= 1;

        // if lives is 2 enable right engine
        // if lives is 1 enable left engine
        if( _lives == 2 )
        {
            _rightEngine.SetActive(true);
        }
        if( _lives == 1 )
        {
            _leftEngine.SetActive(true);
        }

        _uIManager.UpdateLives(_lives);  // also handles UI text display


        if (_lives < 1)
        {
            // find SpawnManager game component, then assign
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine(_canTripleFireTimeWindow));
    }

    IEnumerator TripleShotPowerDownRoutine(float _tripleFireRemainingTime)
    {
        yield return new WaitForSeconds( _tripleFireRemainingTime );
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedActive(_speedBoostTimeWindow));
    }
    IEnumerator SpeedActive(float _speedBoostRemainingTime )
    {
        _speed *= _speedMultiplier;
        yield return new WaitForSeconds(_speedBoostRemainingTime);
        _speed /= _speedMultiplier;
        _isSpeedBoostActive = false;

    }
    public void ShieldsActive()
    {
        _areShieldsActive = true;
        // enable the shields visualizer
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        
        _uIManager.UpdateScore( _score );
    
    }
}
