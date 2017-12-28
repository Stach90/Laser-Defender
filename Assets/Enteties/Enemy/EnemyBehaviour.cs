using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float health = 150; // how much HP have enemies
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip FireSound; // add sound in the Inspector.
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire ();
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject; // Makes projectiles from our enemies Instantiate makes new projectiles in the scene
		missile.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(FireSound, transform.position); // add sound to fire
	}


	public void OnTriggerEnter2D(Collider2D collider){
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
		health -= missile.GetDamage();
		missile.Hit();
		if (health <= 0) {
			Die ();
			
			}
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	
	}
	
}
