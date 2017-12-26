using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	//Speed by Player moving - change too in Inspector to actually change speed!!
	public float speed = 15.0f;
	//Padding restriction. 
	public float padding = 0.5f;
	public GameObject projectile;
	public float projectileSpeed; //Change value in the inspector.
	public float firingRate = 0.2f;
	public float health = 250f; // hp of the player.

	//numbers that restricts our mathf.clamp, made by Camera.main below in Start
	float xmin;
	float xmax;


	void Start () {
		// function viewporttoworldpoint from 0-1 make size of playspace in vector in distance from left to right
		float distance = transform.position.z - Camera.main.transform.position.z; // its make a 0 number for our deph vector
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	void Fire(){
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject; //Quaternion.identity is deleting rotation on projectile
		beam.rigidbody2D.velocity =  new Vector3(0,projectileSpeed,0); // rigidbody2d.velocity give force for projectile movement
		
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		
		}
		
	
	// Code making Player move.
		if (Input.GetKey(KeyCode.LeftArrow))  {
			// old way to make things move: transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			// another way to moving works, in unity we have predefined Vector3.left:
			
			transform.position += Vector3.left * speed * Time.deltaTime;
			
			
		} else if (Input.GetKey(KeyCode.RightArrow)) {
		
			transform.position += Vector3.right * speed * Time.deltaTime;
		 }
		 
		 // Math.clamp make restriction on our vector to gamespace.
		 float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		}
	

	void OnTriggerEnter2D(Collider2D collider){ //Firing enemies code:
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
