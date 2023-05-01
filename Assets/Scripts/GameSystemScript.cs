using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemScript : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject basicEnemyPrefab;
    public float spawnDelay = 1f; // delay between asteroid spawns
    public float spawnDistance = 2f; // how far beyond the screen edge to spawn asteroids
    public float cameraHeight = 10f;
    public float cameraWidth = 20f;
    // public float cameraWidth = 
    public float entitySpeed = 2f; // speed at which asteroids move downward
    public float asteroidScaleVariety = 0.4f;

    private float xMiddle = 0.0f; // center x of the screen
    private float yMiddle = 0.0f; // center y of the screen
    public float deviationSpeedX = 5f; // deviation of the x speed
    public float deviationSpeedY = 0.2f; // deviation of the y speed
    public float defaultRotationSpeed = 50.0f;
    public float deviationRotationSpeed = 10.0f;


    private float nextSpawnTime;

    void Update()
    {
        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextSpawnTime)
        {

            createAsteroid();
            createEnemy();

            // Set the next spawn time based on the spawn delay
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void createAsteroid(){
            // Calculate a random position along the top edge of the screen
            float xPos = Random.Range(-cameraWidth/2.0f, cameraWidth/2.0f);

            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            float verticalSpawnPos = randomSign * (cameraHeight/2.0f + spawnDistance);
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
            asteroidRigidbody.velocity = direction*entitySpeed;

            // Set size
            float newSize = asteroid.transform.localScale.x + Random.Range(-asteroidScaleVariety, asteroidScaleVariety);
            asteroid.transform.localScale = new Vector3(newSize, newSize, 1.0f);

            // Set rotation speed
            randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            AsteroidScript asteroidScript = asteroid.GetComponent<AsteroidScript>();
            asteroidScript.rotationSpeed = randomSign * (defaultRotationSpeed + Random.Range(-deviationRotationSpeed, deviationRotationSpeed));
    }

    void createEnemy(){
            // Calculate a random position along the top edge of the screen
            float xPos = Random.Range(-cameraWidth/2.0f, cameraWidth/2.0f);

            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            float verticalSpawnPos = randomSign * (cameraHeight/2.0f + spawnDistance);
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
            enemyRigidbody.velocity = direction*entitySpeed;
    }
}
