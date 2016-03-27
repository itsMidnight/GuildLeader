using UnityEngine;
using System.Collections;

public class Bar0Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		////transform.RotateAround (Vector3(12, 15.2, 0), Vector3(1,0,0), 1);
		transform.Rotate (0, 0, Time.deltaTime * 100 );
	}
}
