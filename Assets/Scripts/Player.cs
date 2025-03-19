using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private Vector3 _laserStart = new Vector3(0, 1.05f, 0);  // position above Player object
    [SerializeField]
    private float _fireRate = 0.5f;  // space between firing
    private float _canfire = -1f;   // negative to okay firing starting out


    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // find gamecomponent and assign type
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); 
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager.SpawnManager is missing.");
        }

        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        // press a space key to fire a prefab laser object, to create a laser which moves up
        
        if(Input.GetKeyDown(KeyCode.Space ) && ( Time.time > _canfire))
        {
            FireLaser();
        }

    }
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 location = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(location * _speed * Time.deltaTime);

        // if y position of Player is greater than 0 then set to zero
        // if y is less than -3.8f then set to -3.8f
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0),0);


        // if x position is greater than 11 then set to -11
        // and same for left.
        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canfire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserStart, Quaternion.identity);
    }

    public void Damage()
    {
        _lives -= 1;

        if(_lives < 1)
        {
            // find SpawnManager game component, then assign
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
