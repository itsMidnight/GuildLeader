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
	public double Fame { get; set; }
}

public class PlayerMenu : MonoBehaviour {
	public Player player;
	private Text playerName;
    private Text currentFame;

    private static readonly double PassiveFameMultiplier = 1.0001;

    // Use this for initialization
    void Start () {
		

		this.player = new Player ();
		player.Name = "Test";
		player.Description = "TEEEEEEESSSSTTTT";
		player.Fame = 1;

       // this.currentFame = gameObject.GetComponent<Text>();
       this.currentFame = GameObject.FindGameObjectWithTag("FameText").GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        this.player.Fame *= PassiveFameMultiplier; 
        currentFame.text = String.Format("Current Fame: {0:0.00}", this.player.Fame);
    }

	public void MakePlayer () {
	}

}
