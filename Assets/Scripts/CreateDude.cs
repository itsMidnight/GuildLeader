using UnityEngine;
using System.Collections;

public class CreateDude : MonoBehaviour {
	public GameObject dude;
	public int skintone;
	public int eyecolor;
	public int weapon;
	public int suit;

	public void MakeARandomDude () {
		var gameObj = Instantiate<GameObject>(dude);
		var mydude = gameObj.GetComponent<Character> ();
		mydude.Init();
		mydude.InitializeLookAndFeel(mydude.GetRandomSkinTone(), mydude.GetRandomSuit(), mydude.GetRandomWeapon(), mydude.GetRandomEyes());
	}

	public void MakeASpecificDude () {
		var gameObj = Instantiate<GameObject>(dude);
		var mydude = gameObj.GetComponent<Character>();
		mydude.Init();
		mydude.InitializeLookAndFeel(mydude.GetSpecificSkinTone(skintone), mydude.GetSpecificSuit(suit), mydude.GetSpecificWeapon(weapon), mydude.GetSpecificEyes(eyecolor));
	}
}
