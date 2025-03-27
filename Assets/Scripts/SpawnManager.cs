using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerup;

//    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _randomPowerupRate;

    private bool _stopSpawning = false;

    float _topEdge = 8.0f;
    float _leftEdge = -6.0f;
    float _rightEdge = 6.0f;
    //  float _horizontalRandom;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine(5.0f));
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds.
    // Create a coroutine

    IEnumerator SpawnEnemyRoutine(float waitTime)
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(_leftEdge, _rightEdge), _topEdge, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(waitTime);
 
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        // spawn random every 3-7 seconds
        _randomPowerupRate = Random.Range(3f, 8f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(_leftEdge, _rightEdge), _topEdge, 0);
            int randomPowerUp = Random.Range(0, 3); // 0 or 1 makes 2
            Instantiate(_powerup[randomPowerUp], posToSpawn, Quaternion.identity); // triple_shot;

            yield return new WaitForSeconds(_randomPowerupRate);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
