using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

public class PlayerMenu : MonoBehaviour {


	[Serializable]
	public class Player {
		public string Name { get; set; }

		[XmlElement("PlayerDescription")]
		public string PlayerDescription { get; set; }

//		[XmlElement("Fame")]
//		public int Fame { get; set; }

		//test data
		public Player() {
			this.Name = "You";
			this.PlayerDescription = "are a n00b.";
//			this.Fame = "0";
		}

	}


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


}
