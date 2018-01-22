using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

	public float gizmoSize;



	/*
	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, gizmoSize);
	}
	*/

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, gizmoSize);
	}

}
