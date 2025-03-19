using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    float _topEdge = 8.0f;
    float _leftEdge = -6.0f;
    float _rightEdge = 6.0f;
    float _horizontalRandom;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting coroutine");
        coroutine = SpawnRoutine(5.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds.
    // Create a coroutine

    IEnumerator SpawnRoutine(float waitTime)
    {
        while (_stopSpawning == false)
        {
            // instantiage Enemy

            Vector3 posToSpawn = new Vector3(Random.Range(_leftEdge, _rightEdge), _topEdge, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            
            // yield 5 seconds
            
            yield return new WaitForSeconds(waitTime);
            Debug.Log("waiting" + Time.time);
            



 
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
