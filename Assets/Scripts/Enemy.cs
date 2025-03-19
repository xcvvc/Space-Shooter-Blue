using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4.0f;
    float _bottomEdge = -4.0f;
    float _topEdge = 8.0f;
    float _leftEdge = -6.0f;
    float _rightEdge = 6.0f;
    float _horizontalRandom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }
    void calculateMovement()
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
            Destroy(this.gameObject);

            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
