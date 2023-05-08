using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSystemScript : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject basicEnemyPrefab;
    public float asteroidSpawnDelay = 3f; // delay between asteroid spawns
    public float basicEnemySpawnDelay = 5f; // delay between enemy spawns
    public float spawnDistance = 2f; // how far beyond the screen edge to spawn asteroids
    private float cameraWidth;
    // public float cameraWidth = 
    public float globalEntitySpeed = 1f; // speed at which asteroids move downward
    public float asteroidScaleVariety = 0.4f;

    private float xMiddle = 0.0f; // center x of the screen
    private float yMiddle = 0.0f; // center y of the screen
    public float deviationSpeedX = 5f; // deviation of the x speed
    public float deviationSpeedY = 0.2f; // deviation of the y speed
    public float defaultAsteroidRotationSpeed = 50.0f;
    public float deviationAsteroidRotationSpeed = 30.0f;

    
    public List<UnityEvent> levelsList = new List<UnityEvent> {};
    private int currIndex = 0;

    // private LevelFunction currentLevel;
    

    private float startLevelTime;
    private float nextAsteroidSpawnTime;
    private float nextBasicEnemySpawnTime;

    [SerializeField] private float score = 0.0f;


    void Start(){
        cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        startLevelTime = Time.time;
    }

    void Update()
    {
        levelsList[currIndex].Invoke();
    }

    public void TimeLevelTracker(float levelTime){
        if (Time.time >= startLevelTime + levelTime){
            currIndex = (currIndex+1) % levelsList.Count;
            startLevelTime = Time.time;
            Debug.Log("Level change!");
        }
    }

    public void AsteroidSpawner(){
        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextAsteroidSpawnTime)
        {

            createAsteroid();

            // Set the next spawn time based on the spawn delay
            nextAsteroidSpawnTime = Time.time + asteroidSpawnDelay;
        }
        // return false;
    }

    public void BasicEnemySpawner(){
        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextBasicEnemySpawnTime)
        {

            createEnemy();

            // Set the next spawn time based on the spawn delay
            nextBasicEnemySpawnTime = Time.time + basicEnemySpawnDelay;
        }
        // return false;
    }


    void createAsteroid(){
            // Calculate a random position along the top edge of the screen
            float xPos = Random.Range(-cameraWidth/2.0f, cameraWidth/2.0f);

            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            float verticalSpawnPos = randomSign * (Camera.main.orthographicSize + spawnDistance);
            Vector3 spawnPos = new Vector3(xPos, verticalSpawnPos, 0f);


            // Instantiate a new asteroid prefab at the random position
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

            // Calculate the direction from the asteroid to the center of the screen with some deviation
            float dirX = (xMiddle - xPos) + Random.Range(-deviationSpeedX, deviationSpeedX);
            float dirY = (yMiddle - verticalSpawnPos) + Random.Range(-deviationSpeedY, deviationSpeedY);
            Vector2 direction = new Vector2(dirX, dirY).normalized;


            // Set the asteroid's parent to this spawner object for organization
            asteroid.transform.parent = transform;
            Rigidbody2D asteroidRigidbody = asteroid.GetComponent<Rigidbody2D>();
            asteroidRigidbody.velocity = direction*globalEntitySpeed;

            // Set size
            float newSize = asteroid.transform.localScale.x* (1+Random.Range(-asteroidScaleVariety, asteroidScaleVariety));
            asteroid.transform.localScale = new Vector3(newSize, newSize, 1.0f);

            // Set rotation speed
            randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            AsteroidScript asteroidScript = asteroid.GetComponent<AsteroidScript>();
            asteroidScript.rotationSpeed = randomSign * (defaultAsteroidRotationSpeed + Random.Range(-deviationAsteroidRotationSpeed, deviationAsteroidRotationSpeed));
    }

    void createEnemy(){
            // Calculate a random position along the top edge of the screen
            float xPos = Random.Range(-cameraWidth/2.0f, cameraWidth/2.0f);

            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            float verticalSpawnPos = randomSign * (Camera.main.orthographicSize + spawnDistance);
            Vector3 spawnPos = new Vector3(xPos, verticalSpawnPos, 0f);


            // Instantiate a new enemy prefab at the random position
            GameObject enemy = Instantiate(basicEnemyPrefab, spawnPos, Quaternion.identity);

            // Calculate the direction from the enemy to the center of the screen with some deviation
            float dirX = (xMiddle - xPos) + Random.Range(-deviationSpeedX, deviationSpeedX);
            float dirY = (yMiddle - verticalSpawnPos) + Random.Range(-deviationSpeedY, deviationSpeedY);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // Set the enemy's parent to this spawner object for organization
            enemy.transform.parent = transform;
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            enemyRigidbody.velocity = direction*globalEntitySpeed;
    }

    public void AddScore(float s){
        score += s;
    }
}
