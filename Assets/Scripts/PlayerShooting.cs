using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour {

	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);

	public GameObject bulletPrefab;
	int bulletLayer;

	public float fireDelay = 0.25f;
	float cooldownTimer = 0;

	void Start() {
		bulletLayer = gameObject.layer;
	}

	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		// Input.GetButton("Fire1")
		if( Input.GetKeyDown("space") && cooldownTimer <= 0 ) {
			// SHOOT!
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * bulletOffset;

			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
			bulletGO.layer = bulletLayer;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
        if (!col.CompareTag("playerBullet"))
        {
            // Destroy the bullet
            Destroy(col.gameObject);

			// Game Over
			// Debug.Log("Game Over!");
			// SceneManager.LoadScene("End");
        }
    }
}
