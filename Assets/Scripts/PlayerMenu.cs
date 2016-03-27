using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

public class Player {
	public string Name { get; set; }
	public string Description { get; set; }
	public int Fame { get; set; }
}

public class PlayerMenu : MonoBehaviour {
	public Player player;
	private Text playerName;


	// Use this for initialization
	void Start () {
		

		Player p = new Player ();
		p.Name = "Test";
		p.Description = "TEEEEEEESSSSTTTT";
		p.Fame = 1;
	}

	// Update is called once per frame
	void Update () {

	}

	public void MakePlayer () {
	}

}
