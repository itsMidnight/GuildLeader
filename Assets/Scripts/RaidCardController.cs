using UnityEngine;
using System.Collections;

public class RaidCardController : MonoBehaviour 
{
	PanelController raidPanel;

	// Use this for initialization
	void Start () 
	{
		raidPanel = GetComponentInParent<PanelController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		Debug.Log ("clicked " + gameObject.name);
	}
}
