using UnityEngine;
using System.Collections;

public class ADudeMoveScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	private bool dead = false;

	public float baseSpeed;


	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		//if (dead == true) {
		//	//end game and pass back fail (0)
		//}

		//Debug.Log ("Horizontal: " + (Input.GetAxis ("Mouse X")));
		//Debug.Log ("Vertical: " + (Input.GetAxis ("Mouse Y")));

		rb2d.AddForce (new Vector2 (Input.GetAxis ("Mouse X") * baseSpeed, 0));
		rb2d.AddForce (new Vector2 (0, Input.GetAxis ("Mouse Y") * baseSpeed));

		if (dead) {
			
		}
		if (!dead) {
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "Goal") {
			dead = false;
			Debug.Log ("Not dead!");
		} else {
			dead = true;
			//Debug.Log ("You're dead!");
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		dead = true;
		//Debug.Log ("You're dead!");
	}


}
