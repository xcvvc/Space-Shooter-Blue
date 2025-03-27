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

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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
            // Destroy(GameObject.FindWithTag("Enemy"));
        

            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

         //   GameObject gamePlayer = GameObject.FindWithTag("Player");
         //   if( gamePlayer != null )
         //   {
         //       Player playerScore = gamePlayer.transform.GetComponent<Player>();
         //     if( playerScore != null )
         //     {
         //        playerScore.AddScore();
         //     }
         //   }

            if( _player )
            {
                _player.AddScore(10);
            }
            Destroy(this.gameObject);
        }

    }
}
