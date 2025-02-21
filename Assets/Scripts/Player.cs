using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // speed variable for Player movement
    // move Vector3 _speed 3.5f meters per second
    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position set to new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        Vector3 location = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(location * _speed * Time.deltaTime);

        // if y position of Player is greater than 0 then set to zero
        // if y is less than -3.8f then set to -3.8f
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0),0);

        //if (transform.position.y >= 0)
        //{   transform.position = new Vector3(transform.position.x, 0, 0);   }
        //else if (transform.position.y <= -3.5f)
        //{   transform.position = new Vector3(transform.position.x, -3.5f, 0);   }

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
}
