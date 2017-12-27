using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	
	// Putted in the Inspector:
	public GameObject enemyPrefab;
	
	// Variables for our enemies:
	public float width = 10f;
	public float height = 5f;
	public float speed= 5f;
	public float spawnDelay = 0.5f;
	//restriction for moving enemypositions.
	private bool movingRight =  false;
	private float xmax;
	private float xmin;

	void Start () {
	
	float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
	// Camera.main.viewportotoworldpoint give as numbers for our edge.
	Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
	Vector3 righBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera)); //Watch for 1 in vector this make the "right" diffrence!
		xmax = righBoundary.x;
		xmin = leftBoundary.x;
		SpawnUntilFull();
	}
	
			
			
		
	
	
	void SpawnEnemies(){
	foreach( Transform child in transform){
		// Make a new enemy at start, to make a function the GameObject remember to make variable with "as GameObject"!
		GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
		// Makes a folder for our enemies:
		enemy.transform.parent = child;
	
	
		}
	}
	
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
		// Make a new enemy, to make a function the GameObject remember to make variable with "as GameObject"!
		GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
		// Makes a folder for our enemies:
		enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()){
		Invoke ("SpawnUntilFull", spawnDelay); // Invoke is making delay between spawning enemies.
		}
	}
	
	
	
	public void OnDrawGizmos(){
	Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	
	void Update () {
		//Vector3.right/left!!! is made to transform position and direction in speed and time. 
		if(movingRight){
		transform.position += Vector3.right * speed * Time.deltaTime; //Time.deltatime give is fps speed to our speed by relatively this same in different fps count!!
		} else {
		transform.position += Vector3.left * speed * Time.deltaTime;
	}
	
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
	// transform.postiion.x is current position of our GameObject!!
	
	// II is "OR" logic, ! is "NOT" in logic
	if(leftEdgeOfFormation < xmin){
		movingRight = true;
	// our if statement reverse left and right movemenet of our enemies by reversing movingRight variable.
		}else if(rightEdgeOfFormation > xmax){
			movingRight = false;
			}
	
		if(AllMembersDead()){
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}

	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	
	bool AllMembersDead(){ 
	 foreach(Transform childPositionGameObject in transform){
	 	if (childPositionGameObject.childCount > 0){
	 		return false;
	 	}
	 }
	 return true;
	}
}