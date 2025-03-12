using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private float _lowView = 7.0f;


    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }
    void calculateMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // if higher than _outOfView destroy laser !! up is positive
        if (transform.position.y > _lowView)
        {
            Destroy(this.gameObject);
        }
    }
}
