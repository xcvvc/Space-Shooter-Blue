using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    float _bottomEdge = -3.5f;

    [SerializeField]
    private AudioClip _explosionSoundClip, _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is missing.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
            _audioSource.Play();
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _bottomEdge)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                _audioSource.clip = _explosionSoundClip;
                _audioSource.Play();
                player.Damage();
            }

            Destroy(this.gameObject, 2.8f);
        }

    }
}
