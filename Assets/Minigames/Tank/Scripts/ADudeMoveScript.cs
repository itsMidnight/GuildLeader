
using UnityEngine;
using System.Collections;

public class ADudeMoveScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	public bool dead = false;
	public bool gameOver = false;

	public float baseSpeed;



	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {


		rb2d.AddForce (new Vector2 (Input.GetAxis ("Horizontal") * baseSpeed, 0));
		rb2d.AddForce (new Vector2 (0, Input.GetAxis ("Vertical") * baseSpeed));

		//rb2d.AddForce (new Vector2 (Input.GetAxis ("Mouse X") * baseSpeed, 0));
		//rb2d.AddForce (new Vector2 (0, Input.GetAxis ("Mouse Y") * baseSpeed));



	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "Goal" || col.name == "mazeEdge") {
			dead = false;
		} else {
			dead = true;
		}
	
		LevelStateManager m = GameObject.FindGameObjectWithTag("LevelMgr").GetComponent<LevelStateManager>();
		RaidData temp = new RaidData();
		temp.isWin = !dead;
		m.EndLevel(temp);
		//SceneManager.LoadScene(SelectAMazeLevel.gameReturnLevel);

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.name == "Goal" || col.name == "mazeEdge") {
			dead = false;
			//Debug.Log ("Not dead!");
		} else {
			dead = true;

		}

	}


}
