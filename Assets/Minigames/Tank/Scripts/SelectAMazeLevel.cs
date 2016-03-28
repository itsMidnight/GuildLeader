using UnityEngine;
using System.Collections;

public class SelectAMazeLevel : MonoBehaviour {

	public GameObject maze0;
	public GameObject maze1;
	public GameObject maze2;
	public GameObject border;
	public GameObject myDude;
	public GameObject myGoal;


	public static readonly int gameReturnLevel = 1;


	// Use this for initialization
	void Start (/*int whichMaze*/) {

		GameObject loadedMaze;
		GameObject loadedBorder;
		GameObject loadedDude;
		GameObject loadedGoal;



		int whichMaze = Random.Range (0, 3);

		Debug.Log ("Maze " + whichMaze); 

		switch (whichMaze) {
		case 0:
			//init/load maze 0
			loadedMaze = (Instantiate (maze0, transform.position, transform.rotation) as GameObject);

			break;

		case 1:
			//load maze 1
			loadedMaze = (Instantiate(maze1, transform.position, transform.rotation ) as GameObject);

			break;


		case 2:
			//load maze 2
			loadedMaze = (Instantiate(maze2, new Vector3(0,0,0), transform.rotation ) as GameObject);

			break;

		default:
			Debug.Log ("No level selected");
			Application.Quit ();
			break;
		}

			
	}
	
	// Update is called once per frame
	void Update () {
		//if 

		//Debug.Log(
	
	}
}
