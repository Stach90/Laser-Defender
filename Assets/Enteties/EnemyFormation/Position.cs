using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	//Draw Gizmo that helps put our formation of enemies
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position,1);
	
	}



}
