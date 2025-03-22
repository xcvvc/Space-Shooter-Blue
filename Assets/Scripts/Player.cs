using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
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


    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private float _canTripleFireTimeWindow = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        // find gamecomponent and assign type
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager.SpawnManager is missing.");
        }

        transform.position = new Vector3(0, 0, 0);
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
    }

    public void Damage()
    {
        _lives -= 1;

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
        // start the power down coroutine for triple shot
        StartCoroutine(TripleShotPowerDownRoutine(_canTripleFireTimeWindow));
    }
    // IEnumerator TripleShotPowerDownRoutine   wait 5 seconds and set to false

    IEnumerator TripleShotPowerDownRoutine(float _TripleFireRemainingTime)
    {
        yield return new WaitForSeconds( _TripleFireRemainingTime );
        _isTripleShotActive = false;
    }
}
