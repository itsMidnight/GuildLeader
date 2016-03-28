using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

//This function takes a Win/loss bool from the end of a minigame and uses that
//to display the appropriate message in a popup window.

public class WinOrLoss : MonoBehaviour 
{
	private Text GameResult;
	private Text NetFame;
	//private Text NetCharacter; //deprecated
	private List<string> LossList;
	private string ResultContainer;
	private static float result;
	private bool GameWin = false; //test variable, will need to be sent from the minigame
	private int FameContainer;
	private int WonFame = 1000; //test variable, will need to be pulled from GM.raids
	private decimal WinPercent = 0.75m; //test variable, will need to be pulled from GM.raids
		
	

	void Start () {
		this.LossList = new List<string> ();
		this.LossList.Add ("WE DID IT.");
		this.LossList.Add ("Woot!");
		this.LossList.Add ("Great success.");
		this.LossList.Add ("This guild sucks.");
		this.LossList.Add ("You guys said you were good...");
		this.LossList.Add ("Well, that's a wipe.");
	} 

	int WriteToPanel ()
	{
		if (GameWin) {
			result = UnityEngine.Random.Range (0.0f, 2.0f);
			result = (int)Math.Round(result);
			ResultContainer = LossList[(int)result];
		} else {
			result = UnityEngine.Random.Range (3.0f, 5.0f);
			result = (int)Math.Round(result);
			ResultContainer = LossList[(int)result];
		}
		if (WonFame > 0) {
			FameContainer = (int) Decimal.Multiply (WonFame, WinPercent);
		} else {
			FameContainer = (int) WonFame;
		}

		GameResult = gameObject.transform.FindChild ("Result Report").gameObject.GetComponent<Text> ();
		NetFame = gameObject.transform.FindChild ("Fame Count").gameObject.GetComponent<Text> ();
		//NetCharacter = gameObject.transform.FindChild ("Character Gained Or Lost").gameObject.GetComponent<Text> (); // deprecated
		GameResult.text = ResultContainer; 
		if (WonFame > 0) {
			NetFame.text = "+ " + FameContainer;
		} else {
			NetFame.text = "- " + FameContainer;
		}
		return FameContainer;


	}

}