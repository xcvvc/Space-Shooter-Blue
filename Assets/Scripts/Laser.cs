using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private float _topEdge = 6.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > _topEdge)
        {
            // if this has a parent, destroy parent
            // Laser tripleLaser = this.GetComponentInParent<Laser>();
            // if( tripleLaser != null )  // or maybe transform.parent != null

            if( transform.parent != null )
            {
                // Destroy(tripleLaser.gameObject); // or maybe transform.parent.gameObject
                Destroy(transform.parent.gameObject);
            }
                Destroy(this.gameObject);
        }
    }
}
