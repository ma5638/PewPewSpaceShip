using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemyScript : MonoBehaviour
{
    public int health = 1;
    public int points = 20;


    public GameObject bulletPrefab;
    public float bulletOffset = 0.8f;
    private float coolDown = 0.5f;
    public float fireCoolDown = 4.0f;

    // Reference to the mainCamera
    private Camera mainCamera;
    private float destroyOffset = 4f;

    void Start()
    {
        mainCamera = Camera.main;
        // find player object (so we can look at it in the Update)
        GameObject playerObject = GameObject.FindWithTag("player");
    }

    void Update()
    {


        coolDown -= Time.deltaTime;

        if (!IsVisibleFromCameraWithOffset() || health <= 0.0f)
        {
            if (health <= 0.0f)
            {
                GameObject gameController = GameObject.FindWithTag("GameController");
                GameSystemScript gameSystem = gameController.GetComponent<GameSystemScript>();
                gameSystem.AddScore(points);
            }
            // Destroy the asteroid game object
            Destroy(gameObject);
        }

        if (coolDown <= 0.0f){
            float scale = transform.localScale.y;
            coolDown = fireCoolDown;
            if (IsVisibleFromCamera()){
                Vector3 bulletPosition = transform.position + transform.up * (scale*gameObject.GetComponent<CircleCollider2D>().radius*2 + bulletOffset);
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
            }
        }
    }

    private bool IsVisibleFromCameraWithOffset()
    {
        // Get the bounds of the camera view
        Bounds camBounds = new Bounds(mainCamera.transform.position, new Vector3(mainCamera.orthographicSize * 2f * mainCamera.aspect, mainCamera.orthographicSize * 2f, 50f));
        camBounds.Expand(destroyOffset);
        // Check if the asteroid is within the mainCamera's view
        return camBounds.Intersects(GetComponent<SpriteRenderer>().bounds);
    }

    bool IsVisibleFromCamera()
    {
        // Get the bounds of the camera view
        var planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Check if the asteroid is within the camera's view
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<SpriteRenderer>().bounds);
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
