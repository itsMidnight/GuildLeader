using UnityEngine;
using System.Collections;

public class RaidController : MonoBehaviour 
{
	public GameObject raidCard;
	private float lowest_pos = -25;
	private int num_cards = 0;
	private
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCard()
	{
		RectTransform rectTrans;
		Transform trans;
		GameObject new_card = Instantiate (raidCard);

		rectTrans = new_card.GetComponent<RectTransform> ();
		rectTrans.SetParent(this.gameObject.transform);
		rectTrans.sizeDelta = new Vector2 (245f, 418f);
		rectTrans.localPosition = new Vector3 (2.5f, -25f - (float)(rectTrans.sizeDelta.y * num_cards));

		num_cards++;
	}
}
