using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4.0f;
    float _bottomEdge = -3.5f;
    float _topEdge = 5.5f;
    float _leftEdge = -8.0f;
    float _rightEdge = 8.0f;
    float _horizontalRandom;

    [SerializeField]
    private GameObject _laserEnemyPrefab;

    private Vector3 _laserStart = new Vector3(0, -0.75f, 0);  // position above Player object
    [SerializeField]
    private float _fireRate;  // space between firing 3 to 7 seconds random set in Start()
    private float _canFire = -1f;   // negative to okay firing starting out
    private bool _enemyAlive = true;

    private Player _player;
    // handle to animator component
    [SerializeField]
    private Animator _anim;



    private AudioSource _audioSource;   // clip is explosion sound, set in Unity editor
    

    // Start is called before the first frame update
    void Start()
    {
        _fireRate = Random.Range(3.0f, 7.0f);

        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        // null check player
        if ( _player == null )
        {
            Debug.LogError("Player._player is null.");
        }

        // assign the component to anim
        _anim = GetComponent<Animator>();
        if ( _anim == null )
        {
            Debug.LogError("Animator._anim is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if ((Time.time > _canFire) && _enemyAlive)      // _enemyAlive set to true after collision but before Destroy()
        {
            _fireRate = Random.Range(3.0f, 5.0f);
            _canFire = Time.time + _fireRate;
            Instantiate( _laserEnemyPrefab, transform.position + _laserStart, Quaternion.identity);
        }

    }
    void CalculateMovement()
    {

        // move down 4m per second after prefab instantiation.
        // check for survive position at bottom of screen, respawn at top (no destroy, reuse)
        // respawn at top with random horizontal position

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if higher than _outOfView destroy laser !! up is positive
        if (transform.position.y < _bottomEdge)
        {
            _horizontalRandom = UnityEngine.Random.Range(_leftEdge, _rightEdge);
            transform.position = new Vector3(_horizontalRandom, _topEdge, 0);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            // trigger the anim
            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0;

            _audioSource.Play();

            _enemyAlive = false;
            Destroy(this.gameObject, 2.8f );
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);


            if( _player )
            {
                _player.AddScore(10);
            }
            // trigger the anim
            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());  // prevents a second hit and score while exploding
            _enemyAlive = false;                  // prevents firing laser
            Destroy(this.gameObject, 2.8f );
        }

    }
}
