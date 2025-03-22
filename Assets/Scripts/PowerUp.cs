using UnityEngine;


public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    float _bottomEdge = -3.5f;
    // float _topEdge = 5.5f;
    // float _leftEdge = -8.0f;
    // float _rightEdge = 8.0f;
    // float _horizontalRandom;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when leave screen, destroy this object
        if(transform.position.y < _bottomEdge)
        {
            Destroy(this.gameObject);
        }
    }

    // OnTriggerCollision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // communicate with the player script, through other
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
                //                Player player = other.transform.GetComponent<Player>();
            }

            Destroy(this.gameObject);
        }
    }
    // Only be collectable by the Player (HINT: Use Tags
    // on collision destroy.
}
