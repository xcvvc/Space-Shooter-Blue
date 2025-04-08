using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if( _spawnManager == null )
        {
            Debug.LogError("Spawn_Manager.SspawnManager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rotate object on z'ed axis
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime );
    }

    // check for laser collision of type Trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);  // the laser

        _spawnManager.StartSpawning();
        Destroy(this.gameObject, 0.5f );  // the asteroid

    }
    // instantiage explosion at the position of the astroid
    // destroy the explosion after 3 seconds
}
