using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 1;
    public float rotationSpeed = 50.0f;
    public int points = 10;

    // Reference to the Camera.main
    private float destroyOffset = 4f;

    // Update is called once per frame
    void Update()
    {
        
        // transform.position += (Vector3)velocity * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        // Check out of bounds?
        if (!IsVisibleFromCamera() || health <= 0.0f)
        {
            // Destroy the asteroid game object
            Destroy(gameObject);
            if (health <= 0.0f)
            {
                GameObject gameController = GameObject.FindWithTag("GameController");
                GameSystemScript gameSystem = gameController.GetComponent<GameSystemScript>();
                gameSystem.AddScore(points);
            }
        }
        
    }
    private bool IsVisibleFromCamera()
    {
        // Get the bounds of the camera view
        Bounds camBounds = new Bounds(Camera.main.transform.position, new Vector3(Camera.main.orthographicSize * 2f * Camera.main.aspect, Camera.main.orthographicSize * 2f, 50f));
        camBounds.Expand(destroyOffset);
        // Check if the asteroid is within the Camera.main's view
        return camBounds.Intersects(GetComponent<SpriteRenderer>().bounds);
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("playerBullet"))
        {
            // Apply damage to the enemy's health
            health--;

            // Destroy the bullet
            Destroy(col.gameObject);
        }
    }
}
